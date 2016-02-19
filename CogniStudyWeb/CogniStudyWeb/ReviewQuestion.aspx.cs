using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Parse;
using CogniTutor.UserControls;

namespace CogniTutor
{
    public partial class ReviewQuestion : CogniPage
    {
        public ParseObject[] Questions { get { return (ParseObject[])Session["Questions"]; } set { Session["Questions"] = value; } }
        public ParseObject[] QuestionContents { get { return (ParseObject[])Session["QuestionContents"]; } set { Session["QuestionContents"] = value; } }
        public ParseObject[] QuestionData { get { return (ParseObject[])Session["QuestionData"]; } set { Session["QuestionData"] = value; } }
        public ParseObject Bundle = null;
        public string passageText = "";
        public List<string> AlreadyVisited
        {
            get
            {
                if (Session["AlreadyVisited"] == null)
                    Session["AlreadyVisited"] = new List<string>();
                return (List<string>)Session["AlreadyVisited"];
            }
            set
            {
                Session["AlreadyVisited"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override async Task OnStart()
        {
            if (!(UserType == UserTypes.Moderator || UserType == UserTypes.Admin))
                Response.Redirect("QuestionArena");
            if (!IsPostBack)
            {
                AlreadyVisited.Clear();
                await NewQuestion();
            }
        }

        private async Task NewQuestion()
        {
            Questions = await GetQuestions();
            if (Questions == null)
            {
                lbNoResults.Visible = true;
                pnlAll.Visible = false;
                return;
            }
            QuestionContents = await GetQuestionContents();
            QuestionData = await GetQuestionData();
            if (Bundle != null)
            {
                passageText = Bundle.Get<string>("passageText");
            }
            int idx = 1;
            for (int i = 0; i < Questions.Length; i++)
            {
                QuestionBlock questionBlock = (QuestionBlock)LoadControl("~/UserControls/QuestionBlock.ascx");
                questionBlock.FillContents(Questions[i], QuestionContents[i], QuestionData[i], idx++);
                pnlQuestions.Controls.Add(questionBlock);

                AlreadyVisited.Add(Questions[i].ObjectId);
            }
        }

        private async Task<ParseObject[]> GetQuestions()
        {
            ParseObject question1 = await GetQuestion();
            if (question1 == null) return null;
            if (question1.Get<bool>("inBundle"))
            {
                Bundle = question1.Get<ParseObject>("bundle");
                await Bundle.FetchAsync();
                IList<ParseObject> questions = Bundle.Get<IList<ParseObject>>("questions");
                foreach (ParseObject question in questions)
                    await question.FetchIfNeededAsync();
                return questions.ToArray();
            }
            else
            {
                return new ParseObject[1] { question1 };
            }
        }

        private async Task<ParseObject> GetQuestion()
        {
            //var contentsQuery = from contents in ParseObject.GetQuery("QuestionContents")
            //                    where contents.Get<ParseObject>("author") != PublicUserData
            //                    select contents;
            var dataQuery = from data in ParseObject.GetQuery("QuestionData")
                            where data.Get<string>("reviewStatus") == Constants.ReviewStatusType.PENDING
                            select data;
            var questionQuery = from question in ParseObject.GetQuery("Question")
                        //where question.Get<bool>("isActive")
                        where !AlreadyVisited.ToArray().Contains(question.ObjectId)
                        orderby question.CreatedAt descending
                        join data in dataQuery on question["questionData"] equals data
                        //join contents in contentsQuery on question["questionContents"] equals contents
                        select question;
            ParseObject res = null;
            try
            {
                res = await questionQuery.FirstAsync();
            }
            catch (ParseException exc)
            {
                if (AlreadyVisited.Count == 0)
                {
                    AlreadyVisited.Clear();
                }
                else
                {
                    return null;
                }
            }
            if (res == null)
                res = await GetQuestion();
            return res;
        }

        private async Task<ParseObject[]> GetQuestionData()
        {
            ParseObject[] data = new ParseObject[Questions.Length];
            for(int i = 0; i < data.Length; i++) {
                data[i] = Questions[i].Get<ParseObject>("questionData");
                await data[i].FetchIfNeededAsync();
            }
            return data;
        }

        private async Task<ParseObject[]> GetQuestionContents()
        {
            ParseObject[] contents = new ParseObject[Questions.Length];
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = Questions[i].Get<ParseObject>("questionContents");
                await contents[i].FetchIfNeededAsync();
            }
            return contents;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < Questions.Length; i++)
            {
                ParseObject review = new ParseObject("Review");
                review["approved"] = true;
                review["comment"] = tbComments.Text;
                review["reviewerId"] = PublicUserData.ObjectId;
                tasks.Add(review.SaveAsync());
                QuestionData[i]["reviewStatus"] = Constants.ReviewStatusType.APPROVED;
                QuestionData[i]["newlyApproved"] = true;
                QuestionData[i].AddToList("reviews", review);
                tasks.Add(QuestionData[i].SaveAsync());
                Questions[i]["isActive"] = true;
                tasks.Add(QuestionData[i].SaveAsync());
            }
            Task.WaitAll(tasks.ToArray());
            RegisterAsyncTask(new PageAsyncTask(NewQuestion));
        }

        protected void btnDeny_Click(object sender, EventArgs e)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < Questions.Length; i++)
            {
                ParseObject review = new ParseObject("Review");
                review["approved"] = false;
                review["comment"] = tbComments.Text;
                review["reviewerId"] = PublicUserData.ObjectId;
                tasks.Add(review.SaveAsync());
                QuestionData[i]["reviewStatus"] = Constants.ReviewStatusType.DENIED;
                QuestionData[i].AddToList("reviews", review);
                tasks.Add(QuestionData[i].SaveAsync());
                Questions[i]["isActive"] = false;
                tasks.Add(Questions[i].SaveAsync());
            }
            Task.WaitAll(tasks.ToArray());
            RegisterAsyncTask(new PageAsyncTask(NewQuestion));
        }

        protected void btnSkip_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(NewQuestion));
        }
    }
}