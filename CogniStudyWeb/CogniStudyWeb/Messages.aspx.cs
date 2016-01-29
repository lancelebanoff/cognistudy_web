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
        public string TheirName { get { if (TheirPublicData == null) return ""; else return TheirPublicData.Get<string>("displayName"); } }
        public ParseObject TheirPublicData;
        public ParseObject Conversation;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override async Task OnStart()
        {
            if (!this.IsPostBack)
            {

            }
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
                dr["LastMessage"] = await GetFirstMessageText(conv);
                dr["TheirUserID"] = tempTheirPublicData.ObjectId;
                dt.Rows.Add(dr);
            }
            repConversations.DataSource = dt;
            repConversations.DataBind();
            if (Session["ConversationUserId"] != null)
            {
                TheirPublicData = await GetStudentPublicData(Session["ConversationUserId"].ToString());
                Conversation = await GetThisConversation(TheirPublicData.Get<string>("baseUserId"));
                ParseRelation<ParseObject> relation = Conversation.GetRelation<ParseObject>("messages");
                IEnumerable<ParseObject> messages = await relation.Query.FindAsync();
                DataTable messageData = InitMessageTable();
                foreach (ParseObject mes in messages)
                {
                    DataRow dr = messageData.NewRow();
                    dr["Text"] = mes.Get<string>("text");
                    messageData.Rows.Add(dr);
                }
                repMessages.DataSource = messageData;
                repMessages.DataBind();
            }
        }

        public DataTable InitConversationTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TheirName");
            dt.Columns.Add("LastMessage");
            dt.Columns.Add("TheirUserID");
            return dt;
        }

        public DataTable InitMessageTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text");
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
            ParseQuery<ParseObject> query = ParseObject.GetQuery("PublicUserData");
            ParseObject student = await query.GetAsync(studentID);
            return student;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(SendMessage));
        }

        protected async Task SendMessage()
        {
            if (TheirPublicData == null)
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
            tbType.Text = "";

        }

        protected async Task<ParseObject> CreateNewMessage()
        {
            ParseObject message = new ParseObject("Message");
            message["text"] = tbType.Text;
            message["receiverBaseUserId"] = TheirPublicData.Get<string>("baseUserId");
            message["senderBaseUserId"] = UserID;
            await message.SaveAsync();
            return message;
        }

        protected async Task<ParseObject> FindOrCreateNewConversation()
        {
            ParseObject conversation = await GetThisConversation(TheirPublicData.Get<string>("baseUserId"));
            if (conversation != null)
            {
                return conversation;
            }
            else
            {
                conversation = new ParseObject("Conversation");
                conversation["publicUserData1"] = PublicUserData;
                conversation["publicUserData2"] = TheirPublicData;
                conversation["baseUserId1"] = PublicUserData.Get<string>("baseUserId");
                conversation["baseUserId2"] = TheirPublicData.Get<string>("baseUserId");
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
        }



    }
}