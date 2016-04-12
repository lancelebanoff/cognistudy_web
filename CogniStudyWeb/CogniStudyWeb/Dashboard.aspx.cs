using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Parse;
using System.Threading.Tasks;

namespace CogniTutor
{
    public partial class Dashboard : CogniPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("yo");
        }

        protected override async Task OnStart()
        {
            IList<PublicUserData> students = (await PrivateTutorData.Students.FetchAllIfNeededAsync()).ToList();
            grdMyStudents.DataSource = students;
            grdMyStudents.DataBind();
            if (students.Count == 0)
            {
                lbNoStudents.Visible = true;
            }
            else 
            {
                lbNoStudents.Visible = false;
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("hello");
            RegisterAsyncTask(new PageAsyncTask(FillResults));
        }

        public async Task FillResults()
        {
            if (String.IsNullOrEmpty(TextBox1.Text))
            {
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("hello again");
                IEnumerable<PublicUserData> data = (await PublicUserData.Search(TextBox1.Text)).Take(5);
                //await data.FetchAllIfNeededAsync();
                System.Diagnostics.Debug.WriteLine("hello agaiagainn");
                Repeater1.DataSource = data;
                Repeater1.DataBind();
            }
        }

        protected void grdMyStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string TheirObjectId = (string)grdMyStudents.DataKeys[index]["ObjectId"];
            if (e.CommandName == "Message")
            {
                Session["ConversationUserId"] = TheirObjectId;
                Response.Redirect("Messages");
            }
            else if (e.CommandName == "SeeProfile")
            {
                Response.Redirect("StudentProfile.aspx?StudentId=" + TheirObjectId);
            }
        }


    }
}