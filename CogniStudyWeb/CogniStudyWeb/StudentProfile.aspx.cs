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
            btnAcceptStudent.Visible = !Common.ParseContains(PrivateTutorData.Students, StudentPublicData)
                && Common.ParseContains(PrivateTutorData.RequestsFromStudents, StudentPublicData);
        }

        protected void btnRequestStudent_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(() => PrivateTutorData.SendRequestToStudent(StudentPublicData: StudentPublicData, tutor: Tutor)));
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            Session["ConversationUserId"] = StudentPublicDataID;
            Response.Redirect("Messages");
        }

        protected void btnAcceptStudent_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(() => PrivateTutorData.AcceptStudentRequest(StudentPublicData: StudentPublicData, TutorPublicData: PublicUserData)));
        }

        protected void btnBlockStudent_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(() => PrivateTutorData.BlockStudent(StudentPublicData: StudentPublicData)));
        }

        protected void btnRemoveStudent_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(() => PrivateTutorData.RemoveStudent(StudentPublicData: StudentPublicData, TutorPublicData: PublicUserData)));
        }
    }
}