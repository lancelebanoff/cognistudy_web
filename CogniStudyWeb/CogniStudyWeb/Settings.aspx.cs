using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using Parse;

namespace CogniTutor
{
    public partial class Settings : CogniPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override async Task OnStart()
        {
            if (UserType != Constants.UserType.ADMIN)
                Response.Redirect("Dashboard");
            if (!IsPostBack)
            {
                await LoadEverything();
            }
        }

        protected async Task LoadEverything() 
        {
            grdTutors.DataSource = await PublicUserData.AllTutors();
            grdTutors.DataBind();
        }

        protected void grdTutors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Message")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string TheirObjectId = (string)grdTutors.DataKeys[index]["ObjectId"];
                Session["ConversationUserId"] = TheirObjectId;
                Response.Redirect("Messages");
            }
            else if (e.CommandName == "SeeProfile")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string TheirObjectId = (string)grdTutors.DataKeys[index]["ObjectId"];
                Response.Redirect("Profile.aspx?tutorId=" + TheirObjectId);
            }
            else if (e.CommandName == "ToggleModerator")
            {
                string TheirObjectId = e.CommandArgument.ToString();
                PublicUserData pud = AsyncHelpers.RunSync(() => PublicUserData.GetById(TheirObjectId));
                if (pud.UserType == Constants.UserType.TUTOR)
                    pud.UserType = Constants.UserType.MODERATOR;
                else if (pud.UserType == Constants.UserType.MODERATOR)
                    pud.UserType = Constants.UserType.TUTOR;
                AsyncHelpers.RunSync(() => pud.SaveAsync());
            }
            AsyncHelpers.RunSync(LoadEverything);
        }
    }
}