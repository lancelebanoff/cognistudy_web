using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor.UserControls
{
    public partial class QuestionBlock : System.Web.UI.UserControl
    {
        public Question Question;
        public QuestionContents QuestionContents;
        public QuestionData QuestionData;
        public int Index { get { return (int)ViewState["Index"]; } set { ViewState["Index"] = value; } }
        public bool Checkable;
        public int SelectedAnswer { get { return (int)ViewState["SelectedAnswer"]; } set { ViewState["SelectedAnswer"] = value; } }
        public bool IsAnswered { get { return SelectedAnswer != -1; } }
        public bool IsCorrect { get { return SelectedAnswer == QuestionContents.CorrectAnswer; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            SelectedAnswer = rblAnswers.SelectedIndex;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        internal void FillContents(Question question, QuestionContents contents, QuestionData data, int idx)
        {
            Question = question;
            QuestionContents = contents;
            QuestionData = data;
            Index = idx;

            if (Checkable)
            {
                for (int i = 0; i < QuestionContents.Answers.Count; i++)
                {
                    ListItem item = new ListItem(QuestionContents.Answers[i]);
                    rblAnswers.Items.Add(item);
                }
                rblAnswers.DataBind();
            }
        }
    }
}