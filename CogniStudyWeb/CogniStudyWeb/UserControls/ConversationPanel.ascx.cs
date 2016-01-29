using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor.UserControls
{
    public partial class ConversationPanel : System.Web.UI.UserControl
    {
        public string TheirName { get { return lbTheirName.Text; } set { lbTheirName.Text = value; } }
        public string LastMessage { get { return lbLastMessage.Text; } set { lbLastMessage.Text = value; } }
        public string TheirUserID { get { return lbTheirUserID.Text; } set { lbTheirUserID.Text = value; } }

        public CogniPage mPage;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            mPage = this.Page as CogniPage;
            base.OnInit(e);
        }

        protected void btnChangeConversation_Click(object sender, EventArgs e)
        {
            lbLastMessage.Text = "you clicked me";
            mPage.Session["ConversationUserId"] = TheirUserID;
        }
    }
}