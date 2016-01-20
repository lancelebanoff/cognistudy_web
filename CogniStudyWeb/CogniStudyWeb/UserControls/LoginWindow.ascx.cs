using Parse;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor.UserControls
{
    public partial class LoginWindow : System.Web.UI.UserControl
    {
        public CogniPage mPage;
        //public static SqlConnection SqlConnection1 = new SqlConnection(CogniPage.GetConnectionString());

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            mPage = this.Page as CogniPage;
            base.OnInit(e);
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                mPage.RegisterAsyncTask(new PageAsyncTask(LogIn));
                // Login was successful.
                mPage.Session["Email"] = tbLoginEmail.Text.ToLower();
                mPage.Session["Password"] = tbLoginPassword.Text;
                mPage.Redirect("MyStudents.aspx");
            }
            catch (Exception ex)
            {
                // The login failed. Check the error to see why.
                string s = ex.ToString();
                Console.WriteLine(s);

                if (ex.Message.Contains("invalid login parameters"))
                    s = "";
            }
        }
        private async Task LogIn()
        {
            await ParseUser.LogInAsync(tbLoginEmail.Text.ToLower(), tbLoginPassword.Text);
        }

        public void CreateSession(int UserID)
        {
            //Page.Session["SessionID"] = newSessionID;
            //Page.Session["SessionPW"] = newSessionPW;
        }
    }
}