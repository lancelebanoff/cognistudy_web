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
            bool success = AsyncHelpers.RunSync<bool>(LogIn);
            if (success)
            {
                mPage.Session["Email"] = tbLoginEmail.Text.ToLower();
                mPage.Session["Password"] = tbLoginPassword.Text;
                Response.Redirect("Dashboard.aspx");
            }
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
                // Not tutor
                else if (!Constants.UserType.IsTutor((await ParseUser.CurrentUser.Get<PublicUserData>("publicUserData").FetchAsync()).UserType))
                {
                    lblLoginError.Visible = true;
                    lblLoginError.Text = "Only tutors may use the website. If you are a student, then you can open CogniStudy through your mobile device.";
                    return false;
                }
                // Didn't pass registration test
                else if (ParseUser.CurrentUser.Get<int>("registrationTestScore") < 7)
                {
                    lblLoginError.Visible = true;
                    lblLoginError.Text = "You failed to acheive a sufficient score on the registration test.";
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
                    lblLoginError.Text = "There was an unexpected problem with your login. Please try again.";
                    return false;
                }
            }
        }
    }
}