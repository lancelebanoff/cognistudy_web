using Parse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor
{
    public partial class QuestionArena : CogniPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override async Task OnStart()
        {
            DataTable dt = CreateStatusTable();
            IEnumerable<ParseObject> myQuestions = await GetMyQuestions();
            foreach (ParseObject question in myQuestions)
            {
                await question.FetchIfNeededAsync();
                ParseObject contents = await question.Get<ParseObject>("questionContents").FetchIfNeededAsync();
                ParseObject data = await question.Get<ParseObject>("questionData").FetchIfNeededAsync();
                DataRow dr = dt.NewRow();
                dr["subject"] = question.Get<string>("subject");
                dr["category"] = question.Get<string>("category");
                dr["questionText"] = contents.Get<string>("questionText");
                List<object> answers = contents.Get<List<object>>("answers");
                string strAnswers = String.Join("\r\n", answers);
                dr["answers"] = strAnswers;
                dr["reviewStatus"] = data.Get<string>("reviewStatus");
                dt.Rows.Add(dr);
            }
            grdStatus.DataSource = dt;
            grdStatus.DataBind();
        }

        protected void grdStatus_DataBinding(object sender, EventArgs e)
        {
        }

        protected async Task<IEnumerable<ParseObject>> GetMyQuestions()
        {
            var contentsQuery = from contents in new ParseQuery<QuestionContents>()
                                where contents.Author == PublicUserData
                                select contents;
            var questionQuery = from question in new ParseQuery<Question>()
                                orderby question.CreatedAt descending
                                //join data in dataQuery on question["questionData"] equals data
                                join contents in contentsQuery on question["questionContents"] equals contents
                                select question;
            return await questionQuery.FindAsync();
        }

        protected DataTable CreateStatusTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("subject", typeof(string));
            dt.Columns.Add("category", typeof(string));
            dt.Columns.Add("questionText", typeof(string));
            dt.Columns.Add("answers", typeof(string));
            dt.Columns.Add("reviewStatus", typeof(string));
            return dt;
        }


    }
}