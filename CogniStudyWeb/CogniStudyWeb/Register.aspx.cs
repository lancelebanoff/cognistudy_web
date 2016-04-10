using Parse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor
{
    public partial class Register : CogniPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override async Task OnStart()
        {
        }

        private void GoToRegistrationTest()
        {
            RegistrationInfo info = new RegistrationInfo();
            info.FirstName = tbFirstName.Text;
            info.LastName = tbLastName.Text;
            info.Email = tbEmail.Text;
            info.Password = tbPassword.Text;

            //Response.Redirect("RegisterSuccess.aspx");
            Session["RegistrationInfo"] = info;
            Response.Redirect("RegistrationTest");
        }

        #region This Code used to validate
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;
            GoToRegistrationTest();
        }

        private bool Valid()
        {
            lblError.Visible = false;
            if (tbFirstName.Text == "")
            {
                lblError.Text = "First Name is required";
                lblError.Visible = true;
            }
            else if (tbLastName.Text == "")
            {
                lblError.Text = "Last Name is required";
                lblError.Visible = true;
            }
            else if (tbEmail.Text == "")
            {
                lblError.Text = "Email is required";
                lblError.Visible = true;
            }
            else if (tbPassword.Text == "")
            {
                lblError.Text = "Password is required";
                lblError.Visible = true;
            }
            //else if (tbPhoneNumber.Text == "")
            //{
            //    lblError.Text = "Phone Number is required";
            //    lblError.Visible = true;
            //}
            //else if (tbZipCode.Text == "")
            //{
            //    lblError.Text = "Zip Code is required";
            //    lblError.Visible = true;
            //}
            //else if (tbStreetAddress.Text == "")
            //{
            //    lblError.Text = "Street Address is required";
            //    lblError.Visible = true;
            //}
            //else if (tbCity.Text == "")
            //{
            //    lblError.Text = "City is required";
            //    lblError.Visible = true;
            //}
            //else if (ddState.SelectedValue == "")
            //{
            //    lblError.Text = "State is required";
            //    lblError.Visible = true;
            //}
            //else if (tbPassword.Text != tbPasswordRetype.Text)
            //{
            //    lblError.Text = "Passwords do not match";
            //    lblError.Visible = true;
            //}
            //else if (!cbTerms.Checked)
            //{
            //    lblError.Text = "You must agree to the Terms and Conditions";
            //    lblError.Visible = true;
            //}
            else if (AsyncHelpers.RunSync<bool>(() => EmailExists(email: tbEmail.Text)))
            {
                lblError.Text = "That email is not available";
                lblError.Visible = true;
            }

            return !lblError.Visible;
        }

        private async Task<bool> EmailExists(string email)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "email", email}
            };
            bool res = await ParseCloud.CallFunctionAsync<bool>("doesEmailExist", parameters);
            return res;
        }
        #endregion
    }
}