using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor.UserControls
{
    public partial class SwitchEditor : System.Web.UI.UserControl
    {
        public string OriginalText { get; set; }
        public string Text { get { return tbQuestion.Text; } set { tbQuestion.Text = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}