using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor.UserControls
{
    public partial class CheckableQuestionBlock : System.Web.UI.UserControl
    {
        public Question Question;
        public QuestionContents QuestionContents;
        public QuestionData QuestionData;
        public int Index { get { return (int)ViewState["Index"]; } set { ViewState["Index"] = value; } }
        public bool Checkable { get { return (bool)ViewState["Checkable"]; } set { ViewState["Checkable"] = value; } }
        public int SelectedAnswer
        {
            get
            {
                for (int i = 0; i < rblAnswers.Items.Count; i++)
                    if (rblAnswers.Items[i].Selected)
                        return i;
                return -1;
            }
        }
        public int CorrectAnswer { get { return (int)ViewState["CorrectAnswer"]; } set { ViewState["CorrectAnswer"] = value; } }
        public bool IsAnswered { get { return SelectedAnswer != -1; } }
        public bool IsCorrect { get { return SelectedAnswer == CorrectAnswer; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            //SelectedAnswer = rblAnswers.SelectedIndex;
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
            CorrectAnswer = QuestionContents.CorrectAnswer;

            Image1.ImageUrl = QuestionContents.Keys.Contains("image") ? QuestionContents.Get<Parse.ParseFile>("image").Url.ToString() : "";
            lbQuestion.Text = QuestionContents.Get<string>("questionText");

            item1.Text = item1.Value = QuestionContents.Answers[0];
            item2.Text = item2.Value = QuestionContents.Answers[1];
            item3.Text = item3.Value = QuestionContents.Answers[2];
            item4.Text = item4.Value = QuestionContents.Answers[3];
            if (QuestionContents.Answers.Count == 5)
            {
                item5.Text = item5.Value = QuestionContents.Answers[4];
                item5.Attributes.Remove("class");
            }
            else
            {
                item5.Attributes.Add("class", "hidden");
            }
        }
    }
}