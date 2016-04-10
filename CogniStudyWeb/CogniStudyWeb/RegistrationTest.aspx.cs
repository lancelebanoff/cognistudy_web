using CogniTutor.UserControls;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor
{
    public partial class RegistrationTest : CogniPage
    {
        public RegistrationInfo MyRegistrationInfo { get { return (RegistrationInfo)Session["RegistrationInfo"]; } set { Session["RegistrationInfo"] = value; } }
        public Question[] Questions { get { return (Question[])Session["Questions"]; } set { Session["Questions"] = value; } }
        public QuestionContents[] QuestionContents { get { return (QuestionContents[])Session["QuestionContents"]; } set { Session["QuestionContents"] = value; } }
        public QuestionData[] QuestionData { get { return (QuestionData[])Session["QuestionData"]; } set { Session["QuestionData"] = value; } }
        public int QuestionIndex { get { return (int)Session["QuestionIndex"]; } set { Session["QuestionIndex"] = value; } }
        public CheckableQuestionBlock[] QuestionBlocks { get { return (CheckableQuestionBlock[])Session["QuestionBlock"]; } set { Session["QuestionBlock"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            QuestionBlocks = new CheckableQuestionBlock[] {QuestionBlock0,QuestionBlock1,QuestionBlock2,QuestionBlock3,QuestionBlock4,
                    QuestionBlock5,QuestionBlock6,QuestionBlock7,QuestionBlock8,QuestionBlock9};
            base.OnInit(e);
        }

        protected override async Task OnStart()
        {
            //AsyncHelpers.RunSync(() => SignUp(registrationTestScore: 10));
            if (!IsPostBack)
            {
                QuestionIndex = 1;
                await FillTenQuestions();
            }
            
        }

        private async Task FillTenQuestions()
        {
            Questions = await GetQuestions();
            QuestionContents = await GetQuestionContents();
            QuestionData = await GetQuestionData();
            int idx = 1;
            for (int i = 0; i < Questions.Length; i++)
            {
                //QuestionBlock questionBlock = (QuestionBlock)LoadControl("~/UserControls/QuestionBlock.ascx");
                CheckableQuestionBlock questionBlock = QuestionBlocks[i];
                questionBlock.FillContents(Questions[i], QuestionContents[i], QuestionData[i], idx++);
                //pnlQuestions.Controls.Add(questionBlock);
            }
        }

        private async Task<Question[]> GetQuestions()
        {
            Question[] b = await Question.ChooseTenRandomQuestions();
            int a = 0;
            return b;
        }

        private async Task<QuestionData[]> GetQuestionData()
        {
            QuestionData[] data = new QuestionData[Questions.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Questions[i].Get<QuestionData>("questionData");
                await data[i].FetchIfNeededAsync();
            }
            return data;
        }

        private async Task<QuestionContents[]> GetQuestionContents()
        {
            QuestionContents[] contents = new QuestionContents[Questions.Length];
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = Questions[i].QuestionContents;
                await contents[i].FetchIfNeededAsync();
            }
            return contents;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int numCorrect = 0;
            foreach (CheckableQuestionBlock questionBlock in QuestionBlocks)
            {
                if (!questionBlock.IsAnswered)
                {
                    pnlError.Visible = true;
                    return;
                }
                if (questionBlock.IsCorrect)
                    numCorrect++;
            }
            //ParseUser.CurrentUser["registrationTestScore"] = numCorrect;
            //AsyncHelpers.RunSync(ParseUser.CurrentUser.SaveAsync);

            if (numCorrect >= 7)
            {
                AsyncHelpers.RunSync(() => SignUp(registrationTestScore: numCorrect));
                Response.Redirect("RegisterSuccess");
            }
            else
            {
                Response.Redirect("RegisterFailure");
            }
        }

        private async Task SignUp(int registrationTestScore)
        {
            ParseRole adminRole = await Constants.Role.Admin();
            var user = new ParseUser()
            {
                Username = MyRegistrationInfo.Email.ToLower(),
                Password = MyRegistrationInfo.Password,
                Email = MyRegistrationInfo.Email.ToLower(),
            };
            await user.SignUpAsync();

            ParseRole tutorRole = await Constants.Role.Tutor();
            tutorRole.Users.Add(user);
            await tutorRole.SaveAsync();

            PrivateTutorData privateTutorData = new PrivateTutorData();
            privateTutorData.BaseUserId = user.ObjectId;
            privateTutorData.Students = new List<PublicUserData>();
            privateTutorData.RequestsFromStudents = new List<PublicUserData>();
            privateTutorData.Blocked = new List<ParseUser>();
            privateTutorData.ACL = new ParseACL(user);
            privateTutorData.ACL.SetRoleReadAccess(adminRole, true);
            privateTutorData.ACL.SetRoleWriteAccess(adminRole, true);
            await privateTutorData.SaveAsync();

            Tutor tutor = new Tutor();
            tutor.NumQuestionsCreated = 0;
            tutor.NumQuestionsReviewed = 0;
            tutor.BaseUserId = user.ObjectId;
            tutor.Biography = "";
            tutor.PrivateTutorData = privateTutorData;
            tutor.ACL = new ParseACL();
            tutor.ACL.PublicReadAccess = true;
            tutor.ACL.PublicWriteAccess = false;
            tutor.ACL.SetWriteAccess(user, true);
            tutor.ACL.SetRoleReadAccess(adminRole, true);
            tutor.ACL.SetRoleWriteAccess(adminRole, true);
            await tutor.SaveAsync();

            PublicUserData publicUserData = new PublicUserData();
            publicUserData.UserType = Constants.UserType.TUTOR;
            publicUserData.DisplayName = MyRegistrationInfo.FirstName.Trim() + " " + MyRegistrationInfo.LastName.Trim();
            publicUserData.SearchableDisplayName = MyRegistrationInfo.FirstName.Trim().ToLower() + MyRegistrationInfo.LastName.Trim().ToLower();
            publicUserData.BaseUserId = user.ObjectId;
            publicUserData.Tutor = tutor;
            string path = HttpContext.Current.Server.MapPath("~/Images/default_prof_pic.png");
            byte[] pic = File.ReadAllBytes(path);
            publicUserData.ProfilePic = new ParseFile("default-profile-pic", pic);
            publicUserData.ACL = new ParseACL();
            publicUserData.ACL.PublicReadAccess = true;
            publicUserData.ACL.PublicWriteAccess = false;
            publicUserData.ACL.SetWriteAccess(user, true);
            publicUserData.ACL.SetRoleReadAccess(adminRole, true);
            publicUserData.ACL.SetRoleWriteAccess(adminRole, true);
            await publicUserData.SaveAsync();

            user["registrationTestScore"] = registrationTestScore;
            user.ACL = new ParseACL(user);
            user.ACL.SetRoleReadAccess(adminRole, true);
            user["publicUserData"] = publicUserData;
            //user.phoneNumber = tbPhoneNumber.Text;
            //user.zipCode = tbZipCode.Text;
            //user.address = tbStreetAddress.Text;
            //user.address2 = tbAddress2.Text;
            //user.city = tbCity.Text;
            //user.state = ddState.SelectedValue;
            await user.SaveAsync();
        }
    }
}