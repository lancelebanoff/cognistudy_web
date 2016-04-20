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
        public Question[] Questions { get { return (Question[])Session["Questions"]; } set { Session["Questions"] = value; } }
        public QuestionContents[] QuestionContents { get { return (QuestionContents[])Session["QuestionContents"]; } set { Session["QuestionContents"] = value; } }
        public QuestionData[] QuestionData { get { return (QuestionData[])Session["QuestionData"]; } set { Session["QuestionData"] = value; } }
        public QuestionBundle Bundle = null;
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
            if (!(UserType == Constants.UserType.MODERATOR || UserType == Constants.UserType.ADMIN))
                Response.Redirect("QuestionArena");
            if (!IsPostBack)
            {
                AlreadyVisited.Clear();
                await NewQuestion();
            }
        }

        private async Task NewQuestion()
        {
            tbComments.Text = "";
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

        private async Task<Question[]> GetQuestions()
        {
            Question question1 = await GetQuestion();
            if (question1 == null) return null;
            if (question1.Get<bool>("inBundle"))
            {
                Bundle = question1.Get<QuestionBundle>("bundle");
                await Bundle.FetchAsync();
                IList<Question> questions = Bundle.Get<IList<Question>>("questions");
                foreach (Question question in questions)
                    await question.FetchIfNeededAsync();
                return questions.ToArray();
            }
            else
            {
                return new Question[1] { question1 };
            }
        }

        private async Task<Question> GetQuestion()
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "author", PublicUserData.ObjectId },
                { "alreadyVisited", AlreadyVisited },
                { "isAdmin", PublicUserData.UserType == Constants.UserType.ADMIN }
            };
            Question res = await ParseCloud.CallFunctionAsync<Question>("oldestReportedQuestion", parameters);
            if (res != null)
            {
                return res;
            }
            else
            {
                IDictionary<string, object> parameters2 = new Dictionary<string, object>
                {
                    { "author", PublicUserData.ObjectId },
                    { "alreadyVisited", AlreadyVisited },
                    { "isAdmin", PublicUserData.UserType == Constants.UserType.ADMIN }
                };
                Question res2 = await ParseCloud.CallFunctionAsync<Question>("oldestPendingQuestion", parameters2);
                return res2;
            }

            ////var contentsQuery = from contents in new ParseQuery<QuestionContents>()
            ////                    where contents.Get<ParseObject>("author") != PublicUserData
            ////                    select contents;
            //var dataQuery = from data in new ParseQuery<QuestionData>()
            //                where data.Get<string>("reviewStatus") == Constants.ReviewStatusType.PENDING
            //                select data;
            //var questionQuery = from question in new ParseQuery<Question>()
            //            //where question.Get<bool>("isActive")
            //            where !AlreadyVisited.ToArray().Contains(question.ObjectId)
            //            orderby question.CreatedAt descending
            //            join data in dataQuery on question["questionData"] equals data
            //            //join contents in contentsQuery on question["questionContents"] equals contents
            //            select question;
            //Question res = null;
            //try
            //{
            //    res = await questionQuery.FirstAsync();
            //}
            //catch (ParseException exc)
            //{
            //    if (AlreadyVisited.Count == 0)
            //    {
            //        AlreadyVisited.Clear();
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //if (res == null)
            //    res = await GetQuestion();
            //return res;
        }

        private async Task<QuestionData[]> GetQuestionData()
        {
            QuestionData[] data = new QuestionData[Questions.Length];
            for(int i = 0; i < data.Length; i++) {
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
                contents[i] = Questions[i].Get<QuestionContents>("questionContents");
                await contents[i].FetchIfNeededAsync();
            }
            return contents;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Questions.Length; i++)
            {
                ParseObject review = new ParseObject("Review");
                review["approved"] = true;
                review["comment"] = tbComments.Text;
                review["reviewerId"] = PublicUserData.ObjectId;
                AsyncHelpers.RunSync(review.SaveAsync);
                QuestionData[i]["reviewStatus"] = Constants.ReviewStatusType.APPROVED;
                QuestionData[i]["newlyApproved"] = true;
                QuestionData[i].AddToList("reviews", review);
                AsyncHelpers.RunSync(QuestionData[i].SaveAsync); 
                Questions[i]["isActive"] = true;
                AsyncHelpers.RunSync(Questions[i].SaveAsync);
            }
            RegisterAsyncTask(new PageAsyncTask(NewQuestion));
            pnlSuccess.Visible = true;
        }

        protected void btnDeny_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Questions.Length; i++)
            {
                ParseObject review = new ParseObject("Review");
                review["approved"] = false;
                review["comment"] = tbComments.Text;
                review["reviewerId"] = PublicUserData.ObjectId;
                AsyncHelpers.RunSync(review.SaveAsync);
                QuestionData[i]["reviewStatus"] = Constants.ReviewStatusType.DENIED;
                QuestionData[i].AddToList("reviews", review);
                AsyncHelpers.RunSync(QuestionData[i].SaveAsync);
                Questions[i]["isActive"] = false;
                AsyncHelpers.RunSync(Questions[i].SaveAsync);
            }
            RegisterAsyncTask(new PageAsyncTask(NewQuestion));
            pnlSuccess.Visible = true;
        }

        protected void btnSkip_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(NewQuestion));
            pnlSuccess.Visible = false;
        }
    }
}