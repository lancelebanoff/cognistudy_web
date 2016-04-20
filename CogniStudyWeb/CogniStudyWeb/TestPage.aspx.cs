using AjaxControlToolkit;
using Parse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor
{
    public partial class TestPage : CogniPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //ParseRole tutorRole = AsyncHelpers.RunSync<ParseRole>(() => Constants.Role.Tutor());
                //tutorRole.Users.Remove(ParseUser.CurrentUser);
                //AsyncHelpers.RunSync(tutorRole.SaveAsync);
                //ParseRole adminRole = AsyncHelpers.RunSync<ParseRole>(() => Constants.Role.Admin());
                //adminRole.Users.Add(ParseUser.CurrentUser);
                //AsyncHelpers.RunSync(adminRole.SaveAsync);
            }

        }

        protected override async Task OnStart()
        {
            //var moderators = await (from role in ParseRole.Query
            //                        where role.Name == "MODERATOR"
            //                        select role).FirstAsync();
            //var administrators = await (from role in ParseRole.Query
            //                            where role.Name == "ADMIN"
            //                            select role).FirstAsync();
            //var tutors = await (from role in ParseRole.Query
            //                            where role.Name == "TUTOR"
            //                            select role).FirstAsync();
            //moderators.Roles.Add(administrators);
            //await moderators.SaveAsync();
            //tutors.Roles.Add(moderators);
            //await tutors.SaveAsync();
            //administrators.Users.Add(ParseUser.CurrentUser);
            //await administrators.SaveAsync();

            //List<Question> questions = Question.QueryQuestions(null, null);
            //foreach (Question question in questions)
            //{
            //    question.QuestionContents.AuthorId = question.QuestionContents.Author.ObjectId;
            //}
            //await questions.SaveAllAsync();
        }

    }
}