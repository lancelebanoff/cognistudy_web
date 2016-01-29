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
        public ParseObject StudentPublicData;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override async Task OnStart()
        {
            if (!this.IsPostBack)
            {
                string studentID = Request.QueryString["StudentId"];
                StudentPublicData = await GetStudentPublicData(studentID);
            }
        }

        private async Task<ParseObject> GetStudentPublicData(string studentID)
        {
            ParseQuery<ParseObject> query = ParseObject.GetQuery("PublicUserData");
            ParseObject student = await query.GetAsync(studentID);
            return student;
        }

        protected void btnRequestStudent_Click(object sender, EventArgs e)
        {

        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            Session["ConversationUserId"] = StudentPublicDataID;
            Response.Redirect("Messages");
        }
    }
}