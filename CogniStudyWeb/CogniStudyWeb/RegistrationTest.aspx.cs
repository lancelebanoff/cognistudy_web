using CogniTutor.UserControls;
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
    public partial class RegistrationTest : CogniPage
    {
        public Question[] Questions { get { return (Question[])Session["Questions"]; } set { Session["Questions"] = value; } }
        public QuestionContents[] QuestionContents { get { return (QuestionContents[])Session["QuestionContents"]; } set { Session["QuestionContents"] = value; } }
        public QuestionData[] QuestionData { get { return (QuestionData[])Session["QuestionData"]; } set { Session["QuestionData"] = value; } }
        public int QuestionIndex { get { return (int)Session["QuestionIndex"]; } set { Session["QuestionIndex"] = value; } }
        public QuestionBlock[] QuestionBlocks { get { return (QuestionBlock[])Session["QuestionBlock"]; } set { Session["QuestionBlock"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override async Task OnStart()
        {
            if (!IsPostBack)
            {
                QuestionBlocks = new QuestionBlock[] {QuestionBlock0,QuestionBlock1,QuestionBlock2,QuestionBlock3,QuestionBlock4,
                    QuestionBlock5,QuestionBlock6,QuestionBlock7,QuestionBlock8,QuestionBlock9};
            }
            if (Questions == null)
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
                QuestionBlock questionBlock = QuestionBlocks[i];
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
            foreach (QuestionBlock questionBlock in QuestionBlocks)
            {
                if (!questionBlock.IsAnswered)
                {
                    pnlError.Visible = true;
                    return;
                }
                if (questionBlock.IsCorrect)
                    numCorrect++;
            }
            ParseUser.CurrentUser["registrationTestScore"] = numCorrect;
            AsyncHelpers.RunSync(ParseUser.CurrentUser.SaveAsync);

            if (numCorrect >= 7)
            {
                Response.Redirect("RegisterSuccess");
            }
            else
            {
                Response.Redirect("RegisterFailure");
            }
        }
    }
}