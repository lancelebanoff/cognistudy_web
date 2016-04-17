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
        public bool IsEditMode { get { return Session["Question"] != null; } }
        public Question Question { get { return (Question)Session["Question"]; } set { Session["Question"] = value; } }
        public QuestionContents QuestionContents { get { return Question.QuestionContents; } set { Question.QuestionContents = value; } }
        public QuestionData QuestionData { get { return Question.QuestionData; } set { Question.QuestionData = value; } }
        public QuestionBundle Bundle { get { return Question.Bundle; } set { Question.Bundle = value; } }
        public bool IsBundle { get { return cbInBundle.Checked; } }
        public ParseRole TutorRole;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSubject.DataSource = Constants.GetPublicStringProperties(typeof(Constants.Subject));
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, "");
            }

            if (Request.QueryString["success"] == "true")
            {
                pnlSuccess.Visible = true;
            }
            else
            {
                pnlSuccess.Visible = false;
            }
        }

        protected override async Task OnStart()
        {
            TutorRole = await Constants.Role.Tutor();

            if (!IsPostBack)
            {
                if (Session["QuestionObjectId"] != null)
                {
                    Question = await Question.GetFullQuestionById(Session["QuestionObjectId"].ToString());
                    FillWithQuestionInfo();
                }
                else
                {
                    cbInBundle.Enabled = true;
                    pnlExtraQuestions.Visible = true;
                }
            }

        }

        private void FillWithQuestionInfo()
        {
            lbPassageImageUploaded.Visible = false;
            lbImageUploaded.Visible = false;
            lbImageUploaded2.Visible = false;
            lbImageUploaded3.Visible = false;
            cbInBundle.Enabled = false;
            pnlExtraQuestions.Visible = false;

            ddlSubject.SelectedValue = Question.Subject;
            FillCategoryDropdown(Question.Subject);
            ddlCategory.SelectedValue = Question.Category;

            tbQuestion.Text = QuestionContents.QuestionText;
            tbAnswer1.Text = QuestionContents.Answer1;
            tbAnswer2.Text = QuestionContents.Answer2;
            tbAnswer3.Text = QuestionContents.Answer3;
            tbAnswer4.Text = QuestionContents.Answer4;
            if (QuestionContents.Answers.Count == 5)
            {
                cbAnswer5.Checked = true;
                tbAnswer5.Text = QuestionContents.Answer5;
            }
            else
            {
                cbAnswer5.Checked = false;
            }
            SetCorrectAnswer();
            tbExplanation.Text = QuestionContents.Explanation;

            if (Question.InBundle)
            {
                cbInBundle.Checked = true;
                tbPassage.Text = Bundle.PassageText;
                if (Bundle.Image != null)
                {
                    lbPassageImageUploaded.Visible = true;
                }
            }
        }

        private void SetCorrectAnswer()
        {
            if (QuestionContents.CorrectAnswer == 0)
                rbAnswer1.Checked = true;
            else if (QuestionContents.CorrectAnswer == 1)
                rbAnswer2.Checked = true;
            else if (QuestionContents.CorrectAnswer == 2)
                rbAnswer3.Checked = true;
            else if (QuestionContents.CorrectAnswer == 3)
                rbAnswer4.Checked = true;
            else if (QuestionContents.CorrectAnswer == 4)
                rbAnswer5.Checked = true;
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCategoryDropdown();
        }

        private void FillCategoryDropdown()
        {
            ddlCategory.DataSource = Constants.SubjectToCategory[ddlSubject.Text];
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "");
        }

        private void FillCategoryDropdown(string subject)
        {
            ddlCategory.DataSource = Constants.SubjectToCategory[subject];
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "");
        }

        protected void btnSubmitQuestion_Click(object sender, EventArgs e)
        {
            if (!ValidateQuestions())
            {
                pnlError.Visible = true;
                return;
            }
            pnlError.Visible = false;

            if (IsEditMode)
            {
                List<Task> tasks = new List<Task>();
                QuestionContents contents1 = CreateContents1();
                QuestionData data1 = CreateData1();
                Question question1 = CreateQuestion1(contents1, data1);
                if (IsBundle)
                {
                    QuestionBundle bundle = SaveBundle();
                }
                AsyncHelpers.RunSync(contents1.SaveAsync);
                AsyncHelpers.RunSync(data1.SaveAsync);
                AsyncHelpers.RunSync(question1.SaveAsync);
            }
            else
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
            Session["QuestionObjectId"] = null;
            Response.Redirect("UploadQuestion?success=true");
        }

        private bool ValidateQuestions()
        {
            if (ddlSubject.SelectedItem == null || ddlCategory.SelectedItem == null)
            {
                lbError.Text = "Please choose a Subject and Category.";
                return false;
            }
            if(tbQuestion.Text == "")
            {
                lbError.Text = "Please fill in Question text.";
                return false;
            }
            if (tbAnswer1.Text == "" || tbAnswer2.Text == "" || tbAnswer3.Text == "" || tbAnswer4.Text == "" || (cbAnswer5.Checked && tbAnswer5.Text == ""))
            {
                lbError.Text = "Please fill in Answer text.";
                return false;
            }
            if (tbExplanation.Text == "")
            {
                lbError.Text = "Please fill in Explanation text.";
                return false;
            }
            if ( !(rbAnswer1.Checked || rbAnswer2.Checked || rbAnswer3.Checked || rbAnswer4.Checked || (cbAnswer5.Checked && rbAnswer5.Checked)) )
            {
                lbError.Text = "Please mark one of the answers as correct.";
                return false;
            }
            if (cbInBundle.Checked)
            {
                if (!IsEditMode)
                {
                if (tbQuestion2.Text == "" || tbQuestion3.Text == "")
                {
                    lbError.Text = "Please fill in Question text.";
                    return false;
                }
                if (tb2Answer1.Text == "" || tb2Answer2.Text == "" || tb2Answer3.Text == "" || tb2Answer4.Text == "" || (cb2Answer5.Checked && tb2Answer5.Text == "")
                    || tb3Answer1.Text == "" || tb3Answer2.Text == "" || tb3Answer3.Text == "" || tb3Answer4.Text == "" || (cb3Answer5.Checked && tb3Answer5.Text == ""))
                {
                    lbError.Text = "Please fill in Answer text.";
                    return false;
                }
                if (tbExplanation2.Text == "" || tbExplanation3.Text == "")
                {
                    lbError.Text = "Please fill in Explanation text.";
                    return false;
                }
                if (!(rb2Answer1.Checked || rb2Answer2.Checked || rb2Answer3.Checked || rb2Answer4.Checked || (cb2Answer5.Checked && rb2Answer5.Checked))
                    || !(rb3Answer1.Checked || rb3Answer2.Checked || rb3Answer3.Checked || rb3Answer4.Checked || (cb3Answer5.Checked && rb3Answer5.Checked)))
                {
                    lbError.Text = "Please mark one of the answers as correct.";
                    return false;
                }
                }
            }
            return true;
        }

        private QuestionBundle SaveBundle()
        {
            QuestionBundle b;
            if (IsEditMode)
                b = Bundle;
            else
                b = new QuestionBundle();
            b["passageText"] = tbPassage.Text;
            if (FileUpload0.HasFile)
                b["image"] = Upload(FileUpload0);
            b.ACL = new ParseACL();
            b.ACL.PublicReadAccess = true;
            b.ACL.PublicWriteAccess = false;
            b.ACL.SetRoleWriteAccess(TutorRole, true);
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
            qd3.ACL = new ParseACL();
            qd3.ACL.PublicReadAccess = false;
            qd3.ACL.PublicWriteAccess = false;
            qd3.ACL.SetRoleReadAccess(TutorRole, true);
            qd3.ACL.SetRoleWriteAccess(TutorRole, true);
            return qd3;
        }

        private QuestionContents CreateContents3()
        {
            QuestionContents qc3 = new QuestionContents();;
            qc3["correctAnswer"] = CorrectIndex(3);
            qc3["author"] = PublicUserData;
            qc3["questionText"] = tbQuestion3.Text;
            if (cb3Answer5.Checked) qc3["answers"] = new string[] { tb3Answer1.Text, tb3Answer2.Text, tb3Answer3.Text, tb3Answer4.Text, tb3Answer5.Text };
            else qc3["answers"] = new string[] { tb3Answer1.Text, tb3Answer2.Text, tb3Answer3.Text, tb3Answer4.Text };
            qc3["explanation"] = tbExplanation3.Text;
            if (FileUpload3.HasFile)
                qc3["image"] = Upload(FileUpload3);
            qc3.ACL = new ParseACL();
            qc3.ACL.PublicReadAccess = true;
            qc3.ACL.PublicWriteAccess = false;
            qc3.ACL.SetRoleWriteAccess(TutorRole, true);
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
            q3["test"] = false;
            q3["numberInBundle"] = 3;
            q3.ACL = new ParseACL();
            q3.ACL.PublicReadAccess = true;
            q3.ACL.PublicWriteAccess = false;
            q3.ACL.SetRoleWriteAccess(TutorRole, true);
            return q3;
        }

        private QuestionData CreateData2()
        {
            QuestionData qd2 = new QuestionData();;
            qd2["correctResponses"] = 0;
            qd2["totalResponses"] = 0;
            qd2["reviewStatus"] = Constants.ReviewStatusType.PENDING;
            qd2["reviews"] = new List<ParseObject>();
            qd2.ACL = new ParseACL();
            qd2.ACL.PublicReadAccess = false;
            qd2.ACL.PublicWriteAccess = false;
            qd2.ACL.SetRoleReadAccess(TutorRole, true);
            qd2.ACL.SetRoleWriteAccess(TutorRole, true);
            return qd2;
        }

        private QuestionContents CreateContents2()
        {
            QuestionContents qc2 = new QuestionContents();;
            qc2["correctAnswer"] = CorrectIndex(2);
            qc2["author"] = PublicUserData;
            qc2["questionText"] = tbQuestion2.Text;
            if(cb2Answer5.Checked) qc2["answers"] = new string[] { tb2Answer1.Text, tb2Answer2.Text, tb2Answer3.Text, tb2Answer4.Text, tb2Answer5.Text };
            else qc2["answers"] = new string[] { tb2Answer1.Text, tb2Answer2.Text, tb2Answer3.Text, tb2Answer4.Text };
            qc2["explanation"] = tbExplanation2.Text;
            if (FileUpload2.HasFile)
                qc2["image"] = Upload(FileUpload2);
            qc2.ACL = new ParseACL();
            qc2.ACL.PublicReadAccess = true;
            qc2.ACL.PublicWriteAccess = false;
            qc2.ACL.SetRoleWriteAccess(TutorRole, true);
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
            q2["test"] = false;
            q2["numberInBundle"] = 2;
            q2.ACL = new ParseACL();
            q2.ACL.PublicReadAccess = true;
            q2.ACL.PublicWriteAccess = false;
            q2.ACL.SetRoleWriteAccess(TutorRole, true);
            return q2;
        }

        private QuestionData CreateData1()
        {
            QuestionData qd;
            if (IsEditMode)
            {
                qd = QuestionData;
            }
            else
            {
                qd = new QuestionData();
                qd["correctResponses"] = 0;
                qd["totalResponses"] = 0;
                qd["reviews"] = new List<ParseObject>();
                qd.ACL = new ParseACL();
                qd.ACL.PublicReadAccess = false;
                qd.ACL.PublicWriteAccess = false;
                qd.ACL.SetRoleReadAccess(TutorRole, true);
                qd.ACL.SetRoleWriteAccess(TutorRole, true);
            }
            qd["reviewStatus"] = Constants.ReviewStatusType.PENDING;
            return qd;
        }

        private QuestionContents CreateContents1()
        {
            QuestionContents qc;
            if (IsEditMode)
                qc = QuestionContents;
            else
                qc = new QuestionContents();
            qc["correctAnswer"] = CorrectIndex(1);
            qc["author"] = PublicUserData;
            qc["questionText"] = tbQuestion.Text;
            if (cbAnswer5.Checked) qc["answers"] = new string[] { tbAnswer1.Text, tbAnswer2.Text, tbAnswer3.Text, tbAnswer4.Text, tbAnswer5.Text };
            else qc["answers"] = new string[] { tbAnswer1.Text, tbAnswer2.Text, tbAnswer3.Text, tbAnswer4.Text };
            qc["explanation"] = tbExplanation.Text;
            if (FileUpload1.HasFile)
                qc["image"] = Upload(FileUpload1);
            qc.ACL = new ParseACL();
            qc.ACL.PublicReadAccess = true;
            qc.ACL.PublicWriteAccess = false;
            qc.ACL.SetRoleWriteAccess(TutorRole, true);
            return qc;
        }

        private Question CreateQuestion1(QuestionContents qc, QuestionData qd)
        {
            Question q;
            if (IsEditMode)
            {
                q = Question;
            }
            else
            {
                q = new Question();
                q["inBundle"] = false;
                q["questionContents"] = qc;
                q["questionData"] = qd;
                q["test"] = false;
                q["numberInBundle"] = cbInBundle.Checked ? (int?)1 : null;
                q.ACL = new ParseACL();
                q.ACL.PublicReadAccess = true;
                q.ACL.PublicWriteAccess = false;
                q.ACL.SetRoleWriteAccess(TutorRole, true);
            }
            q["subject"] = ddlSubject.Text;
            q["category"] = ddlCategory.Text;
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
        }
    }
}