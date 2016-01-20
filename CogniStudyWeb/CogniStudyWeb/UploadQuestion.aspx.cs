using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Parse;
using System.IO;

namespace CogniTutor
{
    public partial class UploadQuestion : CogniPage
    {
        private int CorrectIndex
        { 
            get 
            {
                if (rbAnswer1.Checked) return 0;
                else if (rbAnswer2.Checked) return 1;
                else if (rbAnswer3.Checked) return 2;
                else if (rbAnswer4.Checked) return 3;
                else if (rbAnswer5.Checked) return 4;
                else return -1;
            } 
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ddlSubject.DataSource = Constants.GetPublicStringProperties(typeof(Constants.Subject));
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, "");
            }
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategory.DataSource = Constants.SubjectToCategory[ddlSubject.Text];
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "");
        }

        protected override async Task OnStart()
        {

        }

        protected void btnSubmitQuestion_Click(object sender, EventArgs e)
        {
            ParseObject qc = new ParseObject("QuestionContents");
            qc["correctAnswer"] = CorrectIndex;
            qc["author"] = ParseUser.CurrentUser.Get<ParseObject>("publicUserData");
            qc["questionText"] = tbQuestion.Text;
            qc["answers"] = new string[] {tbAnswer1.Text, tbAnswer2.Text, tbAnswer3.Text, tbAnswer4.Text, tbAnswer5.Text };
            qc["explanation"] = tbExplanation.Text;
            Task t1 = qc.SaveAsync();

            ParseObject qd = new ParseObject("QuestionData");
            qd["correctResponses"] = 0;
            qd["totalResponses"] = 0;
            Task t2 = qd.SaveAsync();

            ParseObject q = new ParseObject("Question");
            q["subject"] = ddlSubject.Text;
            q["category"] = ddlCategory.Text;
            q["hasPassage"] = false;
            q["questionContents"] = qc;
            q["questionData"] = qd;
            q["reviewStatus"] = Constants.ReviewStatusType.PENDING;
            t1.Wait(); t2.Wait(); 
            Task t3 = q.SaveAsync();
            t3.Wait();
        }

        protected void Upload(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string uploadFileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //string downloadFileName = "user" + Request.QueryString["TutorID"].ToString();
                //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/profile pictures/") + downloadFileName + ".png");
                //Common.ExecuteCommand("update users set PictureFileName='" + downloadFileName
                //    + "' where UserID=" + Request.QueryString["TutorID"]);
                //ReloadPage();
            }
        }
    }
}