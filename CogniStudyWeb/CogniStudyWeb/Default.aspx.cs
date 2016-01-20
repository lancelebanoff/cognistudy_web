using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CogniTutor
{
    public partial class _Default : CogniPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override async Task OnStart()
        {
            //ParseObject gameScore = new ParseObject("GameScore");
            //gameScore["score"] = 1337;
            //gameScore["playerName"] = "Sean Plott";
            //await gameScore.SaveAsync();
            //RegisterAsyncTask(new PageAsyncTask(GetGizmosSvcAsync));
        }
    }
}