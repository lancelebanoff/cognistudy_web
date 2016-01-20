using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Parse;
using System.Web.UI.DataVisualization.Charting;

namespace CogniTutor
{
    public partial class Analytics : CogniPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override async Task OnStart()
        {
            if(!IsPostBack)
            {
                ddlFilterSubject.DataSource = Constants.GetPublicStringProperties(typeof(Constants.Subject));
                ddlFilterSubject.DataBind();

                // Populate series data
                Random random = new Random();
                for (int pointIndex = 0; pointIndex < 10; pointIndex++)
                {
                    Chart3.Series["Series1"].Points.AddY(random.Next(45, 95));
                    Chart3.Series["Series2"].Points.AddY(random.Next(45, 95));
                    Chart3.Series["Series3"].Points.AddY(random.Next(45, 95));
                    Chart3.Series["Series4"].Points.AddY(random.Next(45, 95));
                }

                // Set chart type
                Chart3.Series["Series1"].ChartType = SeriesChartType.StackedBar100;
                Chart3.Series["Series2"].ChartType = SeriesChartType.StackedBar100;
                Chart3.Series["Series3"].ChartType = SeriesChartType.StackedBar100;
                Chart3.Series["Series4"].ChartType = SeriesChartType.StackedBar100;

                // Show point labels
                Chart3.Series["Series1"].IsValueShownAsLabel = true;
                Chart3.Series["Series2"].IsValueShownAsLabel = true;
                Chart3.Series["Series3"].IsValueShownAsLabel = true;
                Chart3.Series["Series4"].IsValueShownAsLabel = true;

                // Disable X axis margin
                Chart3.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;

                // Enable 3D
                Chart3.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart3.DataBind();
            }
        }

        protected void ddlFilterSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(FilterSubject));
        }

        private async Task FilterSubject()
        {
            ParseObject subject = await GetSubject();
            int a = subject.Get<int>("totalResponses");
            int b = subject.Get<int>("correctResponses");
            Chart1.Series[0].Points[0].YValues[0] = subject.Get<int>("correctResponses");
            Chart1.Series[0].Points[1].YValues[0] = subject.Get<int>("totalResponses") - subject.Get<int>("correctResponses");
            Chart1.DataBind();
        }

        private async Task<ParseObject> GetSubject()
        {
            var query = from subj in ParseObject.GetQuery("SubjectStats")
                        where subj.Get<string>("subject") == ddlFilterSubject.Text
                        select subj;
            return await query.FirstAsync();
        }
    }
}