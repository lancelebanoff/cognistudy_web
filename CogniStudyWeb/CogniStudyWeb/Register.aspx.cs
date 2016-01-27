﻿using Parse;
using System;
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
            ParseObject publicUserData = new ParseObject("PublicUserData");
            publicUserData["userType"] = Constants.UserType.TUTOR;
            Task t1 = publicUserData.SaveAsync();

            var user = new ParseUser()
            {
                Username = tbEmail.Text.ToLower(),
                Password = tbPassword.Text,
                Email = tbEmail.Text.ToLower()
            };
            user["publicUserData"] = publicUserData;
            //user["phoneNumber"] = tbPhoneNumber.Text;
            //user["zipCode"] = tbZipCode.Text;
            //user["address"] = tbStreetAddress.Text;
            //user["address2"] = tbAddress2.Text;
            //user["city"] = tbCity.Text;
            //user["state"] = ddState.SelectedValue;
            t1.Wait(); 
            Task t2 = user.SignUpAsync();
            t2.Wait();
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