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
            //mPage.RegisterAsyncTask(new PageAsyncTask(LogIn));
            bool success = AsyncHelpers.RunSync<bool>(LogIn);
            mPage.Session["Email"] = tbLoginEmail.Text.ToLower();
            mPage.Session["Password"] = tbLoginPassword.Text;
            mPage.Redirect("Dashboard.aspx");
        }
        private async Task<bool> LogIn()
        {
            try
            {
                await ParseUser.LogInAsync(tbLoginEmail.Text.ToLower(), tbLoginPassword.Text);

                // Email not verified
                if (!ParseUser.CurrentUser.Get<bool>("emailVerified"))
                {
                    lblLoginError.Visible = true;
                    lblLoginError.Text = "Please check your email to verify your account.";
                    return false;
                }
                // Login was successful.
                else
                {
                    lblLoginError.Visible = false;
                    return true;
                }
            }
            catch (Exception ex)
            {
                // The login failed. Check the error to see why.
                string s = ex.ToString();
                Console.WriteLine(s);

                if (ex.Message.Contains("invalid login parameters"))
                {
                    lblLoginError.Visible = true;
                    lblLoginError.Text = "Username and/or password is incorrect.";
                    return false;
                }
                else
                {
                    lblLoginError.Visible = true;
                    lblLoginError.Text = "There was an unexpected problem with your login. Please contact support@cognitutor.com.";
                    return false;
                }
            }
        }

        public void CreateSession(int UserID)
        {
            //Page.Session["SessionID"] = newSessionID;
            //Page.Session["SessionPW"] = newSessionPW;
        }
    }
}