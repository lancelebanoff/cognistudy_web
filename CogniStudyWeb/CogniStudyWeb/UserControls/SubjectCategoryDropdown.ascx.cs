using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor.UserControls
{
    public partial class SubjectCategoryDropdown : System.Web.UI.UserControl
    {
        public string SelectedSubject { get { return ddlSubject.Text; } }
        public string SelectedCategory { get { return ddlCategory.Text; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSubject.DataSource = Constants.GetPublicStringProperties(typeof(Constants.Subject));
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, "");
            }
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategory.DataSource = Constants.SubjectToCategory[ddlSubject.Text];
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "");
        }
    }
}