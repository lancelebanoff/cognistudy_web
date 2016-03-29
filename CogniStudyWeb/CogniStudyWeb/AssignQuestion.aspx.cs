using AjaxControlToolkit;
using Parse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor
{
    public partial class SuggestQuestion : CogniPage
    {
        public string SelectedQuestionId { get { return Session["SelectedQuestionId"].ToString(); } set { Session["SelectedQuestionId"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected override async Task OnStart()
        {
            if (!IsPostBack)
            {
                ddlSubject.DataSource = Constants.GetPublicStringProperties(typeof(Constants.Subject));
                ddlSubject.DataBind();
                ddlSubject.Items.Insert(0, "");
                cblMyStudents.DataSource = await ParseObject.FetchAllIfNeededAsync(PrivateTutorData.Students);
                cblMyStudents.DataBind();
            }
        }

        //public static List<Question> GetQuestions(out int totalCount)
        //{
        //    Task<IEnumerable<Question>> t = Question.QueryQuestions(null, null);
        //    t.RunSynchronously();
        //    List<Question> list = t.Result.ToList();
        //    totalCount = list.Count;
        //    return list;
        //}

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategory.DataSource = Constants.SubjectToCategory[ddlSubject.Text];
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "");
        }

        protected void btnUpdatePanels_Click(object sender, EventArgs e)
        {
        }

        protected void grdQuestions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Assign")
            {
                SelectedQuestionId = e.CommandArgument.ToString();
                popup.Show();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in cblMyStudents.Items)
            {
                if (item.Selected)
                {
                    AsyncHelpers.RunSync(() => AssignQuestionToStudent(questionObjectId: SelectedQuestionId, studentObjectId: item.Value));
                }
            }
            popup.Hide();
        }

        protected async Task AssignQuestionToStudent(string questionObjectId, string studentObjectId)
        {
            var query = new ParseQuery<PublicUserData>();
            PublicUserData pud = await query.GetAsync(studentObjectId);
            Student student = await pud.Student.FetchIfNeededAsync();
            PrivateStudentData psd = await student.PrivateStudentData.FetchIfNeededAsync();

            Question question = Question.CreateWithoutData<Question>(questionObjectId);
            SuggestedQuestion suggestedQuestion = new SuggestedQuestion();
            suggestedQuestion.Answered = false;
            suggestedQuestion.Question = question;
            suggestedQuestion.Response = null;
            suggestedQuestion.StudentBaseUserId = pud.BaseUserId;
            suggestedQuestion.Tutor = PublicUserData;
            await suggestedQuestion.SaveAsync();
            psd.AssignedQuestions.Add(suggestedQuestion);
            await psd.SaveAsync();

            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "baseUserId", pud.BaseUserId }
            };
            await ParseCloud.CallFunctionAsync<string>("assignQuestion", parameters);
        }

        protected void grdQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //LinkButton lb = e.Row.FindControl("btnAssign") as LinkButton;
            //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);  
        }

    }

    public class TwoString
    {
        public string questionObjectId;
        public string studentObjectId;
        public TwoString(string questionObjectId, string studentObjectId)
        {
            this.questionObjectId = questionObjectId;
            this.studentObjectId = studentObjectId;
        }
    }
}