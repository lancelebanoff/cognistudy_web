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
using System.Drawing;
using CogniTutor.UserControls;

namespace CogniTutor
{
    public partial class Analytics : CogniPage
    {
        UpdatePanel[] updatePanels;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override async Task OnStart()
        {
            updatePanels = new UpdatePanel[] {UpdatePanel1, UpdatePanel2, UpdatePanel3, UpdatePanel4, 
                    UpdatePanel5, UpdatePanel6, UpdatePanel7, UpdatePanel8, UpdatePanel9, UpdatePanel10};
            if(!IsPostBack)
            {
                ddlFilterSubject.DataSource = Constants.GetPublicStringProperties(typeof(Constants.Subject));
                ddlFilterSubject.DataBind();
                ddlFilterSubject.Items.Insert(0, "All Subjects");
                await FilterSubject();

                Random random = new Random();
                for (int pointIndex = 0; pointIndex < 10; pointIndex++)
                {
                    Chart4.Series["Series1"].Points.AddY(random.Next(45, 95));
                }

                // Set series chart type
                Chart4.Series["Series1"].ChartType = SeriesChartType.Line;

                // Set point labels
                Chart4.Series["Series1"].IsValueShownAsLabel = true;

                // Enable X axis margin
                Chart4.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

                // Enable 3D, and show data point marker lines
                //Chart4.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Chart4.Series["Series1"]["ShowMarkerLines"] = "True";
            }
        }

        protected void ddlFilterSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(FilterSubject));
        }

        private async Task FilterSubject()
        {
            if (ddlFilterSubject.Text == "All Subjects")
            {
                int totalResponses = 0;
                int correctResponses = 0;
                List<string> subjects = Constants.GetPublicStringProperties(typeof(Constants.Subject));
                foreach (string subjectName in subjects)
                {
                    ParseObject subject = await GetSubject(subjectName);
                    totalResponses += subject.Get<int>("totalResponses");
                    correctResponses += subject.Get<int>("correctResponses");
                }
                DoughnutChart subjectChart = (DoughnutChart)LoadControl("~/UserControls/DoughnutChart.ascx");
                subjectChart.Chart.Series[0].Points[0].YValues[0] = correctResponses;
                subjectChart.Chart.Series[0].Points[1].YValues[0] = totalResponses;
                subjectChart.Chart.DataBind();
                updatePanels[0].ContentTemplateContainer.Controls.Add(subjectChart);
            }
            else
            {
                DoughnutChart subjectChart = (DoughnutChart)LoadControl("~/UserControls/DoughnutChart.ascx");
                ParseObject subject = await GetSubject(ddlFilterSubject.Text);
                subjectChart.Chart.Series[0].Points[0].YValues[0] = subject.Get<int>("correctResponses");
                subjectChart.Chart.Series[0].Points[1].YValues[0] = subject.Get<int>("totalResponses") - subject.Get<int>("correctResponses");
                subjectChart.Chart.DataBind();
                updatePanels[0].ContentTemplateContainer.Controls.Add(subjectChart);

                string[] categories = Constants.SubjectToCategory[ddlFilterSubject.Text];
                int i = 1;
                foreach (string catName in categories)
                {
                    SingleBarChart catChart = (SingleBarChart)LoadControl("~/UserControls/SingleBarChart.ascx");
                    ParseObject category = await GetCategory(catName);
                    catChart.Chart.Series["Series1"].Points.AddY(category.Get<int>("correctResponses"));
                    catChart.Chart.Series["Series2"].Points.AddY(category.Get<int>("totalResponses") - category.Get<int>("correctResponses"));
                    catChart.Chart.DataBind();
                    updatePanels[i++].ContentTemplateContainer.Controls.Add(catChart);
                }
            }
        }

        private async Task<ParseObject> GetSubject(string subjectName)
        {
            var query = from subj in ParseObject.GetQuery("SubjectStats")
                        where subj.Get<string>("subject") == subjectName
                        select subj;
            return await query.FirstAsync();
        }

        private async Task<ParseObject> GetCategory(string catName)
        {
            var query = from cat in ParseObject.GetQuery("CategoryStats")
                        where cat.Get<string>("category") == catName
                        select cat;
            return await query.FirstAsync();
        }
    }
}