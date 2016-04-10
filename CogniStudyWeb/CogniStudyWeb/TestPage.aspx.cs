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
        }

        protected void Timer1_Tick(object sender, EventArgs e)
    {
        Label1.Text = "Panel refreshed at: " +
DateTime.Now.ToLongTimeString();
    }
    }
}