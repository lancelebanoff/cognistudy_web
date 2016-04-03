using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor
{
    public partial class StudentProfile : CogniPage
    {
        public string StudentName { get { return StudentPublicData.Get<string>("displayName"); } }
        public string StudentPublicDataID { get { return StudentPublicData.ObjectId; } }
        public PublicUserData StudentPublicData { get { return (PublicUserData)Session["StudentPublicData"]; } set { Session["StudentPublicData"] = value; } }
        //public ParseUser StudentUser { get { return (ParseUser)Session["StudentUser"]; } set { Session["StudentUser"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override async Task OnStart()
        {
            if (!this.IsPostBack)
            {
                string studentID = Request.QueryString["StudentId"];
                StudentPublicData = await PublicUserData.GetById(studentID);
                Image1.ImageUrl = StudentPublicData.ProfilePic != null ? StudentPublicData.ProfilePic.Url.ToString() : "Images/default_prof_pic.png";
                await PrivateTutorData.Students.FetchAllIfNeededAsync();
            }
            bool a = Common.ParseContains(PrivateTutorData.Students, StudentPublicData);
            //bool a = Common.ParseContains(PrivateTutorData.Students, StudentPublicData);
            btnBlockStudent.Visible = !Common.ParseContains(PrivateTutorData.Students, StudentPublicData);
            btnRemoveStudent.Visible = Common.ParseContains(PrivateTutorData.Students, StudentPublicData);
            btnStudentAdded.Visible = Common.ParseContains(PrivateTutorData.Students, StudentPublicData);

            btnRequestStudent.Visible = !Common.ParseContains(PrivateTutorData.Students, StudentPublicData)
                && !Common.ParseContains(PrivateTutorData.RequestsFromStudents, StudentPublicData);
            //btnRequestSent.Visible = !Common.ParseContains(PrivateTutorData.Students, StudentPublicData)
            //    && !Common.ParseContains(PrivateTutorData.RequestsFromStudents, StudentPublicData);
            btnAcceptStudent.Visible = !Common.ParseContains(PrivateTutorData.Students, StudentPublicData)
                && Common.ParseContains(PrivateTutorData.RequestsFromStudents, StudentPublicData);
        }

        public int StudentAllTimeAnswered()
        {
            //IDictionary<string, object> parameters = new Dictionary<string, object>
            //{
            //    { "studentBaseId", StudentPublicData.BaseUserId },
            //};
            //Task<int> t = ParseCloud.CallFunctionAsync<int>("getStudentAllTimeAnswered", parameters);
            //t.Wait();
            //return t.Result;
            return 10;
        }

        protected void btnRequestStudent_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(() => PrivateTutorData.SendRequestToStudent(StudentPublicData: StudentPublicData, TutorPublicData: PublicUserData)));
            lbSuccess.Text = "Your request was successfully sent to the student.";
            pnlSuccess.Visible = true;
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            Session["ConversationUserId"] = StudentPublicDataID;
            Response.Redirect("Messages");
        }

        protected void btnAcceptStudent_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(() => PrivateTutorData.AcceptStudentRequest(StudentPublicData: StudentPublicData, TutorPublicData: PublicUserData)));
            lbSuccess.Text = "You are now friends with the student. You can now view their analytics and assign questions to them.";
            pnlSuccess.Visible = true;
        }

        protected void btnBlockStudent_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(() => PrivateTutorData.BlockStudent(StudentPublicData: StudentPublicData)));
        }

        protected void btnRemoveStudent_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(() => PrivateTutorData.RemoveStudent(StudentPublicData: StudentPublicData, TutorPublicData: PublicUserData)));
            lbSuccess.Text = "You are no longer friends with this student.";
            pnlSuccess.Visible = true;
        }
    }
}