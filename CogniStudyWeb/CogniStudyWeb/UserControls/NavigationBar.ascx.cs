using Parse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor
{
    public partial class NavigationBar : System.Web.UI.UserControl
    {
        public CogniPage mPage;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            AsyncHelpers.RunSync(FillNotifications);
            base.OnPreRender(e);
        }

        private async Task FillNotifications()
        {
            if (mPage.LoggedIn)
            {
                var query = mPage.PrivateTutorData.Notifications.Query.OrderByDescending("createdAt").Include("userFrom");
                List<NotificationTutor> Notifications = (await query.FindAsync()).ToList();
                int unseen = 0;
                List<NotificationTutor> FilteredNotifications = new List<NotificationTutor>();
                for (int i = 0; i < Notifications.Count; i++)
                {
                    // If notification is old, remove it
                    if ( (Notifications[i].FirstSeenAt != null && DateTime.Now.Subtract((DateTime)Notifications[i].FirstSeenAt).TotalHours > 1 )
                        || IsDuplicateMessage(FilteredNotifications, Notifications[i]))
                    {
                        await Notifications[i].DeleteAsync();
                    }
                    else
                    {
                        if (Notifications[i].FirstSeenAt == null)
                            unseen++;
                        FilteredNotifications.Add(Notifications[i]);
                    }
                }
                listNotifications.DataSource = FilteredNotifications;
                if (unseen > 0)
                {
                    lblNumNotifications.Text = unseen.ToString();
                    lblNumNotifications.Visible = true;
                }
                else
                {
                    lblNumNotifications.Visible = false;
                }
                if (FilteredNotifications.Count == 0)
                {
                    lbNoNotifications.Visible = true;
                }
                else
                {
                    lbNoNotifications.Visible = false;
                }
                DataBind();
            }
        }

        private bool IsDuplicateMessage(List<NotificationTutor> list, NotificationTutor candidate)
        {
            foreach (NotificationTutor notification in list)
                if (notification.Type == Constants.NotificationType.MESSAGE && notification.UserFrom.ObjectId == candidate.UserFrom.ObjectId)
                    return true;
            return false;
        }

        protected override void OnInit(EventArgs e)
        {
            mPage = this.Page as CogniPage;
            base.OnInit(e);
        }

        protected void LogOut(object sender, EventArgs e)
        {
            mPage.Session["Email"] = null;
            mPage.Session["Password"] = null;
            Response.Redirect("Default.aspx");
        }

        protected void listNotifications_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var query = new ParseQuery<NotificationTutor>();
            NotificationTutor notification = AsyncHelpers.RunSync(() => query.GetAsync(e.CommandName));
            if (notification.FirstSeenAt == null)
            {
                notification.FirstSeenAt = DateTime.Now;
                AsyncHelpers.RunSync(notification.SaveAsync);
            }
            if (notification.Type == Constants.NotificationType.MESSAGE)
            {
                Session["ConversationUserId"] = notification.UserFrom.ObjectId;
            }
            Response.Redirect(notification.RedirectLink);
        }
    }
}