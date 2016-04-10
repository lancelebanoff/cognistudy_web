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

        protected void Page_Load(object sender, EventArgs e)
        {
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
        }

        internal void FillContents(Question question)
        {
            Question = question;
            QuestionContents = question.QuestionContents;
            QuestionData = question.QuestionData;
            Index = 1;
        }
    }
}