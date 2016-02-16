using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor.UserControls
{
    public partial class LatexEditor : System.Web.UI.UserControl
    {
        public string Text { get { return FixCKEditorText(tb.Text); } }
        public string AcutalID { get { return tb.ClientID; } }
        public Unit Height
        {
            get { return tb.Height; }
            set { tb.Height = value; }
        }
        public Unit Width
        {
            get { return tb.Width; }
            set { tb.Width = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string FixCKEditorText(string s)
        {
            while (s.IndexOf("<img alt=\"") != -1)
            {
                int start = s.IndexOf("<img alt=\"");
                int end = s.IndexOf("/>", start);
                string imgtag = s.Substring(start, end - start);
                string latex = imgtag.Replace("<img alt=\"", "");
                latex = latex.Substring(0, latex.IndexOf("\" src=\""));
                s = s.Replace(imgtag, "\\(" + latex + "\\)");
            }
            return s;
        }
    }
}