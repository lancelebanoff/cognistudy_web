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
        public bool HasPassage { get { return ddlCategory.Text == Constants.Category.PASSAGE_READING; } }

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
            List<Task> tasks = new List<Task>();
            ParseObject contents1 = CreateContents1();
            ParseObject data1 = CreateData1();
            ParseObject question1 = CreateQuestion1(contents1, data1);

            if(HasPassage)
            {
                ParseObject passage = SavePassage();
                question1["hasPassage"] = true;
                contents1["passage"] = passage;
                ParseObject contents2 = CreateContents2(passage);
                ParseObject data2 = CreateData2();
                ParseObject question2 = CreateQuestion2(contents2, data2);
                ParseObject contents3 = CreateContents3(passage);
                ParseObject data3 = CreateData3();
                ParseObject question3 = CreateQuestion3(contents3, data3);
                tasks.Add(contents1.SaveAsync());
                tasks.Add(contents2.SaveAsync());
                tasks.Add(contents3.SaveAsync());
                tasks.Add(data1.SaveAsync());
                tasks.Add(data1.SaveAsync());
                tasks.Add(data1.SaveAsync());
                foreach (Task t in tasks) t.Wait();
                Task t1 = question1.SaveAsync();
                Task t2 = question2.SaveAsync();
                Task t3 = question3.SaveAsync();
                t1.Wait(); t2.Wait(); t3.Wait();
                passage["questions"] = new ParseObject[] { question1, question2, question3 };
                Task t4 = passage.SaveAsync();
                t4.Wait();
            }
            else
            {
                Task t1 = contents1.SaveAsync();
                Task t2 = data1.SaveAsync();
                t1.Wait(); t2.Wait();
                Task t3 = question1.SaveAsync();
                t3.Wait();
            }
        }

        private ParseObject SavePassage()
        {
            ParseObject p = new ParseObject("Passage");
            p["passageText"] = tbPassage.Text;
            Task t1 = p.SaveAsync();
            t1.Wait();
            return p;
        }

        private ParseObject CreateData3()
        {
            ParseObject qd3 = new ParseObject("QuestionData");
            qd3["correctResponses"] = 0;
            qd3["totalResponses"] = 0;
            return qd3;
        }

        private ParseObject CreateContents3(ParseObject passage)
        {
            ParseObject qc3 = new ParseObject("QuestionContents");
            qc3["correctAnswer"] = CorrectIndex(3);
            qc3["author"] = ParseUser.CurrentUser.Get<ParseObject>("publicUserData");
            qc3["questionText"] = tbQuestion3.Text;
            if (cb2Answer5.Checked) qc3["answers"] = new string[] { tb3Answer1.Text, tb3Answer2.Text, tb3Answer3.Text, tb3Answer4.Text, tb3Answer5.Text };
            else qc3["answers"] = new string[] { tb3Answer1.Text, tb3Answer2.Text, tb3Answer3.Text, tb3Answer4.Text };
            qc3["explanation"] = tbExplanation3.Text;
            qc3["passage"] = passage;
            return qc3;
        }

        private ParseObject CreateQuestion3(ParseObject qc3, ParseObject qd3)
        {
            ParseObject q3 = new ParseObject("Question");
            q3["subject"] = ddlSubject.Text;
            q3["category"] = ddlCategory.Text;
            q3["hasPassage"] = true;
            q3["questionContents"] = qc3;
            q3["questionData"] = qd3;
            q3["reviewStatus"] = Constants.ReviewStatusType.PENDING;
            return q3;
        }

        private ParseObject CreateData2()
        {
            ParseObject qd2 = new ParseObject("QuestionData");
            qd2["correctResponses"] = 0;
            qd2["totalResponses"] = 0;
            return qd2;
        }

        private ParseObject CreateContents2(ParseObject passage)
        {
            ParseObject qc2 = new ParseObject("QuestionContents");
            qc2["correctAnswer"] = CorrectIndex(2);
            qc2["author"] = ParseUser.CurrentUser.Get<ParseObject>("publicUserData");
            qc2["questionText"] = tbQuestion2.Text;
            if(cb2Answer5.Checked) qc2["answers"] = new string[] { tb2Answer1.Text, tb2Answer2.Text, tb2Answer3.Text, tb2Answer4.Text, tb2Answer5.Text };
            else qc2["answers"] = new string[] { tb2Answer1.Text, tb2Answer2.Text, tb2Answer3.Text, tb2Answer4.Text };
            qc2["explanation"] = tbExplanation2.Text;
            qc2["passage"] = passage;
            return qc2;
        }

        private ParseObject CreateQuestion2(ParseObject qc2, ParseObject qd2)
        {
            ParseObject q2 = new ParseObject("Question");
            q2["subject"] = ddlSubject.Text;
            q2["category"] = ddlCategory.Text;
            q2["hasPassage"] = true;
            q2["questionContents"] = qc2;
            q2["questionData"] = qd2;
            q2["reviewStatus"] = Constants.ReviewStatusType.PENDING;
            return q2;
        }

        private ParseObject CreateData1()
        {
            ParseObject qd = new ParseObject("QuestionData");
            qd["correctResponses"] = 0;
            qd["totalResponses"] = 0;
            return qd;
        }

        private ParseObject CreateContents1()
        {
            ParseObject qc = new ParseObject("QuestionContents");
            qc["correctAnswer"] = CorrectIndex(1);
            qc["author"] = ParseUser.CurrentUser.Get<ParseObject>("publicUserData");
            qc["questionText"] = tbQuestion.Text;
            if(cbAnswer5.Checked) qc["answers"] = new string[] { tbAnswer1.Text, tbAnswer2.Text, tbAnswer3.Text, tbAnswer4.Text, tbAnswer5.Text };
            else qc["answers"] = new string[] { tbAnswer1.Text, tbAnswer2.Text, tbAnswer3.Text, tbAnswer4.Text };
            qc["explanation"] = tbExplanation.Text;
            return qc;
        }

        private ParseObject CreateQuestion1(ParseObject qc, ParseObject qd)
        {
            ParseObject q = new ParseObject("Question");
            q["subject"] = ddlSubject.Text;
            q["category"] = ddlCategory.Text;
            q["hasPassage"] = false;
            q["questionContents"] = qc;
            q["questionData"] = qd;
            q["reviewStatus"] = Constants.ReviewStatusType.PENDING;
            return q;
        }

        public int CorrectIndex(int questionindex)
        {
            if (questionindex == 1)
            {
                if (rbAnswer1.Checked) return 0;
                else if (rbAnswer2.Checked) return 1;
                else if (rbAnswer3.Checked) return 2;
                else if (rbAnswer4.Checked) return 3;
                else if (rbAnswer5.Checked) return 4;
                else return -1;
            }
            else if (questionindex == 2)
            {
                if (rb2Answer1.Checked) return 0;
                else if (rb2Answer2.Checked) return 1;
                else if (rb2Answer3.Checked) return 2;
                else if (rb2Answer4.Checked) return 3;
                else if (rb2Answer5.Checked) return 4;
                else return -1;
            }
            else
            {
                if (rb3Answer1.Checked) return 0;
                else if (rb3Answer2.Checked) return 1;
                else if (rb3Answer3.Checked) return 2;
                else if (rb3Answer4.Checked) return 3;
                else if (rb3Answer5.Checked) return 4;
                else return -1;
            }
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