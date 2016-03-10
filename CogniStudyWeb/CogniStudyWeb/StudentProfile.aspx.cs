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
                if ((await PrivateTutorData.RequestsFromStudents.FetchAllIfNeededAsync()).Contains(StudentPublicData))
                    btnRequestStudent.Text = "Accept student request";
            }
        }

        protected void btnRequestStudent_Click(object sender, EventArgs e)
        {
            if (PrivateTutorData.RequestsFromStudents.Contains(StudentPublicData))
            {
                RegisterAsyncTask(new PageAsyncTask(() => PrivateTutorData.AddStudent(StudentPublicData: StudentPublicData)));
                //PrivateTutorData.AddStudent(StudentPublicData).Wait();
            }
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            Session["ConversationUserId"] = StudentPublicDataID;
            Response.Redirect("Messages");
        }

        //protected void btnBlockStudent_Click(object sender, EventArgs e)
        //{

        //}
    }
}