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
            grdTutors.DataSource = await PublicUserData.AllTutors();
            grdTutors.DataBind();
        }
    }
}