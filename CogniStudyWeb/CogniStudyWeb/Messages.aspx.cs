using CogniTutor.UserControls;
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
    public partial class Messages : CogniPage
    {
        public string TheirName { get { if (RecipientPublicData == null) return ""; else return RecipientPublicData.Get<string>("displayName"); } }
        public ParseObject RecipientPublicData { get { return (ParseObject)Session["RecipientPublicData"]; } set { Session["RecipientPublicData"] = value; } }
        public ParseObject Conversation { get { return (ParseObject)Session["Conversation"]; } set { Session["Conversation"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override async Task OnStart()
        {
            if (!this.IsPostBack)
            {
                await LoadEverything();
            }
        }

        private async Task LoadEverything()
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "scrollPanel", "scrollPanel();", true);
            IEnumerable<ParseObject> conversations = await GetConversations();
            DataTable dt = InitConversationTable();
            foreach (ParseObject conv in conversations)
            {
                //ConversationPanel pnl = (ConversationPanel)LoadControl("~/UserControls/ConversationPanel.ascx");
                //string theirNumber = conv.Get<string>("baseUserId1") == UserID ? "2" : "1";
                //ParseObject tempTheirPublicData = conv.Get<ParseObject>("publicUserData" + theirNumber);
                //await tempTheirPublicData.FetchIfNeededAsync();
                //pnl.TheirName = tempTheirPublicData.Get<string>("displayName");
                //pnl.LastMessage = await GetFirstMessageText(conv);
                //pnl.TheirUserID = tempTheirPublicData.ObjectId;
                //pnlConversations.ContentTemplateContainer.Controls.Add(pnl);
                //pnlConversations.Controls.Add(pnl);

                DataRow dr = dt.NewRow();
                string theirNumber = conv.Get<string>("baseUserId1") == UserID ? "2" : "1";
                ParseObject tempTheirPublicData = conv.Get<ParseObject>("publicUserData" + theirNumber);
                await tempTheirPublicData.FetchIfNeededAsync();
                dr["TheirName"] = tempTheirPublicData.Get<string>("displayName");
                string lastMessage = await GetFirstMessageText(conv);
                dr["LastMessage"] = lastMessage.Length <= 30 ? lastMessage : lastMessage.Substring(0, 30) + "...";
                dr["TheirUserID"] = tempTheirPublicData.ObjectId;
                ParseFile profilePic = tempTheirPublicData.Get<ParseFile>("profilePic");
                dr["ProfilePicUrl"] = profilePic == null ? "Images/default_prof_pic.png" : profilePic.Url.ToString();
                dt.Rows.Add(dr);
            }
            repConversations.DataSource = dt;
            repConversations.DataBind();
            if (Session["ConversationUserId"] != null)
            {
                RecipientPublicData = await GetStudentPublicData(Session["ConversationUserId"].ToString());
                Conversation = await GetThisConversation(RecipientPublicData.Get<string>("baseUserId"));
                if (Conversation == null)
                {

                }
                else
                {
                    ParseRelation<ParseObject> relation = Conversation.GetRelation<ParseObject>("messages");
                    IEnumerable<ParseObject> messages = await relation.Query.OrderBy("createdAt").FindAsync();
                    DataTable messageData = InitMessageTable();
                    foreach (ParseObject mes in messages)
                    {
                        DataRow dr = messageData.NewRow();
                        dr["Text"] = mes.Get<string>("text");
                        dr["WasSentByMe"] = mes.Get<string>("senderBaseUserId") == UserID;
                        dr["SentAt"] = mes.Get<DateTime>("sentAt").ToString();
                        messageData.Rows.Add(dr);
                    }
                    repMessages.DataSource = messageData;
                    repMessages.DataBind();
                    await RemoveNotificationsForConversation();
                }
                tbType.Enabled = true;
                btnSend.Enabled = true;
            }
            else
            {
                tbType.Enabled = false;
                btnSend.Enabled = false;
            }
        }

        private async Task RemoveNotificationsForConversation()
        {
            var query = PrivateTutorData.Notifications.Query.OrderByDescending("createdAt").Include("userFrom");
            List<NotificationTutor> Notifications = (await query.FindAsync()).ToList();
            foreach (NotificationTutor notification in Notifications)
            {
                if (notification.UserFrom.ObjectId == RecipientPublicData.ObjectId)
                    await notification.DeleteAsync();
            }
        }

        public DataTable InitConversationTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TheirName");
            dt.Columns.Add("LastMessage");
            dt.Columns.Add("TheirUserID");
            dt.Columns.Add("ProfilePicUrl");
            return dt;
        }

        public DataTable InitMessageTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text");
            dt.Columns.Add("WasSentByMe");
            dt.Columns.Add("SentAt");
            return dt;
        }

        private async Task<ParseObject> GetThisConversation(string theirUserID)
        {
            var query = from conv in ParseObject.GetQuery("Conversation")
                        where (conv.Get<string>("baseUserId1") == theirUserID || conv.Get<string>("baseUserId2") == theirUserID)
                        && (conv.Get<string>("baseUserId1") == UserID || conv.Get<string>("baseUserId2") == UserID)
                        select conv;
            return await query.FirstOrDefaultAsync();
        }

        private async Task<IEnumerable<ParseObject>> GetConversations()
        {
            var query = from conv in ParseObject.GetQuery("Conversation")
                        where (conv.Get<string>("baseUserId1") == UserID || conv.Get<string>("baseUserId2") == UserID)
                        orderby conv.Get<DateTime>("lastSent") descending
                        select conv;
            return await query.FindAsync();
        }

        private async Task<string> GetFirstMessageText(ParseObject conversation)
        {
            var relation = conversation.GetRelation<ParseObject>("messages");
            var query = from mes in relation.Query
                        orderby mes.CreatedAt descending
                        select mes;
            ParseObject message = await query.FirstAsync();
            return message.Get<string>("text");
        }

        private async Task<ParseObject> GetStudentPublicData(string studentID)
        {
            ParseQuery<PublicUserData> query = new ParseQuery<PublicUserData>();
            ParseObject student = await query.GetAsync(studentID);
            return student;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //RegisterAsyncTask(new PageAsyncTask(SendMessage));
            //Task t = SendMessage();
            //t.Wait();
            AsyncHelpers.RunSync(SendMessage);
            AsyncHelpers.RunSync(LoadEverything);
        }

        protected async Task SendMessage()
        {
            if (RecipientPublicData == null)
                throw new Exception("no user selected");
            ParseObject message = await CreateNewMessage();
            if (Conversation == null)
            {
                Conversation = await FindOrCreateNewConversation();
            }
            ParseRelation<ParseObject> messages = Conversation.GetRelation<ParseObject>("messages");
            messages.Add(message);
            Conversation["lastSent"] = DateTime.Now;
            await Conversation.SaveAsync();

            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "senderBaseUserId", PublicUserData.BaseUserId },
                { "receiverBaseUserId", RecipientPublicData.Get<string>("baseUserId") },
                { "senderName", PublicUserData.DisplayName },
                { "messageText", tbMessage.Text }
            };
            await ParseCloud.CallFunctionAsync<string>("sendMessageNotification", parameters);

            tbMessage.Text = "";
            tbType.Focus();
        }

        protected async Task<ParseObject> CreateNewMessage()
        {
            ParseObject message = new ParseObject("Message");
            message["text"] = tbMessage.Text;
            message["receiverBaseUserId"] = RecipientPublicData.Get<string>("baseUserId");
            message["senderBaseUserId"] = UserID;
            message["sentAt"] = DateTime.UtcNow;
            await message.SaveAsync();
            return message;
        }

        protected async Task<ParseObject> FindOrCreateNewConversation()
        {
            ParseObject conversation = await GetThisConversation(RecipientPublicData.Get<string>("baseUserId"));
            if (conversation != null)
            {
                return conversation;
            }
            else
            {
                conversation = new ParseObject("Conversation");
                conversation["publicUserData1"] = PublicUserData;
                conversation["publicUserData2"] = RecipientPublicData;
                conversation["baseUserId1"] = PublicUserData.Get<string>("baseUserId");
                conversation["baseUserId2"] = RecipientPublicData.Get<string>("baseUserId");
                await conversation.SaveAsync();
                return conversation;
            }
        }

        protected void repConversations_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Change")
            {
                Session["ConversationUserId"] = e.CommandArgument.ToString();
            }
            AsyncHelpers.RunSync(LoadEverything);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            AsyncHelpers.RunSync(LoadEverything);
            tbType.Focus();
        }


    }
}