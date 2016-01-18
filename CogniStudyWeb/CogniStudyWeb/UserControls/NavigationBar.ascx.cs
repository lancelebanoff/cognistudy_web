using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor
{
    public partial class NavigationBar : System.Web.UI.UserControl
    {
        public CogniPage mPage;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void OnInit(EventArgs e)
        {
            mPage = this.Page as CogniPage;
            base.OnInit(e);
        }

        protected void LogOut(object sender, EventArgs e)
        {
            mPage.Session["Email"] = null;
            mPage.Session["Password"] = null;
            Response.Redirect("Default.aspx");
        }
    }
}