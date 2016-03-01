using AjaxControlToolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor
{
    public partial class SuggestQuestion : CogniPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected override async Task OnStart()
        {
            if (!IsPostBack)
            {
                ddlSubject.DataSource = Constants.GetPublicStringProperties(typeof(Constants.Subject));
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, "");
            }
        }

        //public static List<Question> GetQuestions(out int totalCount)
        //{
        //    Task<IEnumerable<Question>> t = Question.QueryQuestions(null, null);
        //    t.RunSynchronously();
        //    List<Question> list = t.Result.ToList();
        //    totalCount = list.Count;
        //    return list;
        //}

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategory.DataSource = Constants.SubjectToCategory[ddlSubject.Text];
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "");
        }

        protected void btnUpdatePanels_Click(object sender, EventArgs e)
        {
        }

        protected void grdQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

    }
}