using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Parse;
using System.Threading.Tasks;

namespace CogniTutor
{
    public partial class MyStudents : CogniPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override async Task OnStart()
        {
            lblText.Text = ParseUser.CurrentUser.Email;
        }



    }
}