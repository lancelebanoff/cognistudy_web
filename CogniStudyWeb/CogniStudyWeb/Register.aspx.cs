using Parse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        private async Task SignUp()
        {
            var user = new ParseUser()
            {
                Username = tbEmail.Text.ToLower(),
                Password = tbPassword.Text,
                Email = tbEmail.Text.ToLower()
            };
            Task t1 = user.SignUpAsync();
            t1.Wait();
            ParseObject privateTutorData = new ParseObject("PrivateTutorData");
            privateTutorData["baseUserId"] = user.ObjectId;
            privateTutorData["requestsFromStudents"] = new List<PublicUserData>();
            Task t2 = privateTutorData.SaveAsync();
            t2.Wait();
            ParseObject tutor = new ParseObject("Tutor");
            tutor["numQuestionsCreated"] = 0;
            tutor["numQuestionsReviewed"] = 0;
            tutor["baseUserId"] = user.ObjectId;
            tutor["privateTutorData"] = privateTutorData;
            Task t3 = tutor.SaveAsync();
            t3.Wait();
            ParseObject publicUserData = new ParseObject("PublicUserData");
            publicUserData["userType"] = Constants.UserType.TUTOR;
            publicUserData["displayName"] = tbFirstName.Text.Trim() + " " + tbLastName.Text.Trim();
            publicUserData["searchableDisplayName"] = tbFirstName.Text.Trim().ToLower() + tbLastName.Text.Trim().ToLower();
            publicUserData["baseUserId"] = user.ObjectId;
            publicUserData["tutor"] = tutor;
            Task t4 = publicUserData.SaveAsync();
            t4.Wait();
            user["publicUserData"] = publicUserData;
            //user["phoneNumber"] = tbPhoneNumber.Text;
            //user["zipCode"] = tbZipCode.Text;
            //user["address"] = tbStreetAddress.Text;
            //user["address2"] = tbAddress2.Text;
            //user["city"] = tbCity.Text;
            //user["state"] = ddState.SelectedValue;
            Task t5 = user.SaveAsync();
            t5.Wait();
            Response.Redirect("RegisterSuccess.aspx");
        }

        #region This Code used to Insert data and Send Email
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Valid())
                return;
            RegisterAsyncTask(new PageAsyncTask(SignUp));
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
            //else if (EmailExists(tbEmail.Text))
            //{
            //    lblError.Text = "That email is not available";
            //    lblError.Visible = true;
            //}

            return !lblError.Visible;
        }
        #endregion
    }
}