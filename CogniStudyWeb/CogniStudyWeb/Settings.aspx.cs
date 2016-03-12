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
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "tutorId", Tutor.ObjectId }
            };
            string s = await ParseCloud.CallFunctionAsync<string>("tutorRequestToStudent", parameters);
            int a = 0;
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            lb.Text = tb.Text;
        }
    }
}