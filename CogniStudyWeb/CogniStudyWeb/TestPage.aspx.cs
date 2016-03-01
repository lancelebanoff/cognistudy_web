using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniStudyWeb
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Data");
                DataRow dr = dt.NewRow();
                dr["Name"] = "apple";
                dr["Data"] = "ha";
                dt.Rows.Add(dr);
                DataRow r = dt.NewRow();
                r["Name"] = "banana";
                r["Data"] = "ho";
                dt.Rows.Add(r);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PopupControlExtender pce = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;

                string behaviorID = "pce_" + e.Row.RowIndex;
                pce.BehaviorID = behaviorID;

                Image img = (Image)e.Row.FindControl("Image1");

                string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
                string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

                img.Attributes.Add("onmouseover", OnMouseOverScript);
                img.Attributes.Add("onmouseout", OnMouseOutScript);
            }
        }

        [System.Web.Services.WebMethodAttribute(),
       System.Web.Script.Services.ScriptMethodAttribute()]
        public static string GetDynamicContent(string contextKey)
        {
            StringBuilder b = new StringBuilder();
            b.Append("hahahaa");
            return b.ToString();
        }
    }
}