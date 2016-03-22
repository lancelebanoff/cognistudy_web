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
        public bool IsBundle { get { return cbInBundle.Checked; } }

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
            QuestionContents contents1 = CreateContents1();
            QuestionData data1 = CreateData1();
            Question question1 = CreateQuestion1(contents1, data1);

            if (IsBundle)
            {
                QuestionBundle bundle = SaveBundle();
                question1["inBundle"] = true;
                question1["bundle"] = bundle;
                QuestionContents contents2 = CreateContents2();
                QuestionData data2 = CreateData2();
                Question question2 = CreateQuestion2(contents2, data2, bundle);
                QuestionContents contents3 = CreateContents3();
                QuestionData data3 = CreateData3();
                Question question3 = CreateQuestion3(contents3, data3, bundle);
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
                bundle["questions"] = new ParseObject[] { question1, question2, question3 };
                Task t4 = bundle.SaveAsync();
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

        private QuestionBundle SaveBundle()
        {
            QuestionBundle b = new QuestionBundle();
            b["passageText"] = tbPassage.Text;
            if (FileUpload0.HasFile)
                b["image"] = Upload(FileUpload0);
            Task t1 = b.SaveAsync();
            t1.Wait();
            return b;
        }

        private QuestionData CreateData3()
        {
            QuestionData qd3 = new QuestionData();;
            qd3["correctResponses"] = 0;
            qd3["totalResponses"] = 0;
            qd3["reviewStatus"] = Constants.ReviewStatusType.PENDING;
            qd3["reviews"] = new List<ParseObject>();
            return qd3;
        }

        private QuestionContents CreateContents3()
        {
            QuestionContents qc3 = new QuestionContents();;
            qc3["correctAnswer"] = CorrectIndex(3);
            qc3["author"] = ParseUser.CurrentUser.Get<ParseObject>("publicUserData");
            qc3["questionText"] = tbQuestion3.Text;
            if (cb2Answer5.Checked) qc3["answers"] = new string[] { tb3Answer1.Text, tb3Answer2.Text, tb3Answer3.Text, tb3Answer4.Text, tb3Answer5.Text };
            else qc3["answers"] = new string[] { tb3Answer1.Text, tb3Answer2.Text, tb3Answer3.Text, tb3Answer4.Text };
            qc3["explanation"] = tbExplanation3.Text;
            if (FileUpload3.HasFile)
                qc3["image"] = Upload(FileUpload3);
            return qc3;
        }

        private Question CreateQuestion3(QuestionContents qc3, QuestionData qd3, ParseObject bundle)
        {
            Question q3 = new Question();;
            q3["subject"] = ddlSubject.Text;
            q3["category"] = ddlCategory.Text;
            q3["inBundle"] = true;
            q3["questionContents"] = qc3;
            q3["questionData"] = qd3;
            q3["bundle"] = bundle;
            q3["isActive"] = false;
            return q3;
        }

        private QuestionData CreateData2()
        {
            QuestionData qd2 = new QuestionData();;
            qd2["correctResponses"] = 0;
            qd2["totalResponses"] = 0;
            qd2["reviewStatus"] = Constants.ReviewStatusType.PENDING;
            qd2["reviews"] = new List<ParseObject>();
            return qd2;
        }

        private QuestionContents CreateContents2()
        {
            QuestionContents qc2 = new QuestionContents();;
            qc2["correctAnswer"] = CorrectIndex(2);
            qc2["author"] = ParseUser.CurrentUser.Get<ParseObject>("publicUserData");
            qc2["questionText"] = tbQuestion2.Text;
            if(cb2Answer5.Checked) qc2["answers"] = new string[] { tb2Answer1.Text, tb2Answer2.Text, tb2Answer3.Text, tb2Answer4.Text, tb2Answer5.Text };
            else qc2["answers"] = new string[] { tb2Answer1.Text, tb2Answer2.Text, tb2Answer3.Text, tb2Answer4.Text };
            qc2["explanation"] = tbExplanation2.Text;
            if (FileUpload2.HasFile)
                qc2["image"] = Upload(FileUpload2);
            return qc2;
        }

        private Question CreateQuestion2(QuestionContents qc2, QuestionData qd2, ParseObject bundle)
        {
            Question q2 = new Question();;
            q2["subject"] = ddlSubject.Text;
            q2["category"] = ddlCategory.Text;
            q2["inBundle"] = true;
            q2["questionContents"] = qc2;
            q2["questionData"] = qd2;
            q2["bundle"] = bundle;
            q2["isActive"] = false;
            return q2;
        }

        private QuestionData CreateData1()
        {
            QuestionData qd = new QuestionData();;
            qd["correctResponses"] = 0;
            qd["totalResponses"] = 0;
            qd["reviewStatus"] = Constants.ReviewStatusType.PENDING;
            qd["reviews"] = new List<ParseObject>();
            return qd;
        }

        private QuestionContents CreateContents1()
        {
            QuestionContents qc = new QuestionContents();
            qc["correctAnswer"] = CorrectIndex(1);
            qc["author"] = ParseUser.CurrentUser.Get<ParseObject>("publicUserData");
            qc["questionText"] = tbQuestion.Text;
            if(cbAnswer5.Checked) qc["answers"] = new string[] { tbAnswer1.Text, tbAnswer2.Text, tbAnswer3.Text, tbAnswer4.Text, tbAnswer5.Text };
            else qc["answers"] = new string[] { tbAnswer1.Text, tbAnswer2.Text, tbAnswer3.Text, tbAnswer4.Text };
            qc["explanation"] = tbExplanation.Text;
            if (FileUpload1.HasFile)
                qc["image"] = Upload(FileUpload1);
            return qc;
        }

        private Question CreateQuestion1(QuestionContents qc, QuestionData qd)
        {
            Question q = new Question();;
            q["subject"] = ddlSubject.Text;
            q["category"] = ddlCategory.Text;
            q["inBundle"] = false;
            q["questionContents"] = qc;
            q["questionData"] = qd;
            q["isActive"] = false;
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

        protected ParseFile Upload(FileUpload fupload)
        {
            string uploadFileName = Path.GetFileName(fupload.PostedFile.FileName);
            FileInfo Finfo = new FileInfo(fupload.PostedFile.FileName);
            string extension = Finfo.Extension.ToLower();
            byte[] data = fupload.FileBytes;
            ParseFile file = new ParseFile("question" + Tutor.Get<int>("numQuestionsCreated") + extension, data);
            Task t = file.SaveAsync();
            t.Wait();
            return file;
            //string downloadFileName = "user" + Request.QueryString["TutorID"].ToString();
            //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Images/profile pictures/") + downloadFileName + ".png");
            //Common.ExecuteCommand("update users set PictureFileName='" + downloadFileName
            //    + "' where UserID=" + Request.QueryString["TutorID"]);
            //ReloadPage();
        }
    }
}