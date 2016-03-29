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
        public Question Question = null;
        public QuestionContents QuestionContents = null;
        public QuestionData QuestionData = null;
        public int Index = -1;
        public bool Checkable = false;
        public bool IsAnswered { get { return rblAnswers.SelectedItem != null; } }
        public bool IsCorrect { get { return rblAnswers.SelectedIndex == QuestionContents.CorrectAnswer; } }

        protected void Page_Load(object sender, EventArgs e)
        {

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