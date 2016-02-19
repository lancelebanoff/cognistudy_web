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
        public ParseObject Question = null;
        public ParseObject QuestionContents = null;
        public ParseObject QuestionData = null;
        public int Index = -1;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        internal void FillContents(ParseObject question, ParseObject contents, ParseObject data, int idx)
        {
            Question = question;
            QuestionContents = contents;
            QuestionData = data;
            Index = idx;
        }
    }
}