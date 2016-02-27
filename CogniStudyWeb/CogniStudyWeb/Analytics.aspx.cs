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
using System.Data;

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
            if (!IsPostBack)
            {
                await FillFilterDropdowns();
            }
            await BuildCharts();
            if(!IsPostBack)
            {
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

        private async Task FillFilterDropdowns()
        {
            ddlFilterSubject.DataSource = Constants.GetPublicStringProperties(typeof(Constants.Subject));
            ddlFilterSubject.DataBind();
            ddlFilterSubject.Items.Insert(0, "All Subjects");
            ddlFilterStudent.DataSource = await GetStudentDataSource();
            ddlFilterStudent.DataBind();
            ddlFilterStudent.Items.Insert(0, "All Students");
        }

        private async Task<object> GetStudentDataSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("studentName", typeof(string));
            dt.Columns.Add("objectId", typeof(string));
            IList<PublicUserData> students = PrivateTutorData.Students;
            await students.FetchAllIfNeededAsync();
            foreach (PublicUserData student in students)
            {
                DataRow dr = dt.NewRow();
                dr["studentName"] = student.DisplayName;
                dr["objectId"] = student.ObjectId;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private async Task BuildCharts()
        {
            if (ddlFilterStudent.Text == "All Students")
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
                    subjectChart.NumCorrect = correctResponses;
                    subjectChart.NumIncorrect = totalResponses - correctResponses;
                    subjectChart.Chart.DataBind();
                    pnlTest.Controls.Add(subjectChart);
                }
                else
                {
                    DoughnutChart subjectChart = (DoughnutChart)LoadControl("~/UserControls/DoughnutChart.ascx");
                    ParseObject subject = await GetSubject(ddlFilterSubject.Text);
                    subjectChart.NumCorrect = subject.Get<int>("correctResponses");
                    subjectChart.NumIncorrect = subject.Get<int>("totalResponses") - subject.Get<int>("correctResponses");
                    subjectChart.Chart.DataBind();
                    pnlTest.Controls.Add(subjectChart);

                    string[] categories = Constants.SubjectToCategory[ddlFilterSubject.Text];
                    int i = 1;
                    foreach (string catName in categories)
                    {
                        SingleBarChart catChart = (SingleBarChart)LoadControl("~/UserControls/SingleBarChart.ascx");
                        ParseObject category = await GetCategory(catName);
                        catChart.SetCorrectIncorrect(category.Get<int>("correctResponses"), category.Get<int>("totalResponses") - category.Get<int>("correctResponses"));
                        catChart.Chart.DataBind();
                        pnlTest.Controls.Add(catChart);
                    }
                }
            }
            else
            {
                PublicUserData publicUserData = await PublicUserData.GetById(ddlFilterStudent.SelectedValue);
                Student student = await publicUserData.Student.FetchIfNeededAsync();
                //PrivateStudentData privateStudentData = await student.PrivateStudentData.FetchIfNeededAsync();
                
                if (ddlFilterSubject.Text == "All Subjects")
                {
                    StudentTotalRollingStats studentTotalRollingStats = await student.StudentTotalRollingStats.FetchIfNeededAsync();
                    int totalResponses =  0;
                    int correctResponses = 0;
                    if(ddlFilterTime.SelectedValue == "PastWeek") {
                        totalResponses =  studentTotalRollingStats.TotalPastWeek;
                        correctResponses = studentTotalRollingStats.CorrectPastWeek;
                    }
                    else if(ddlFilterTime.SelectedValue == "PastMonth") {
                        totalResponses =  studentTotalRollingStats.TotalPastMonth;
                        correctResponses = studentTotalRollingStats.CorrectPastMonth;
                    }
                    else {
                        totalResponses =  studentTotalRollingStats.TotalAllTime;
                        correctResponses = studentTotalRollingStats.CorrectAllTime;
                    }
                    DoughnutChart subjectChart = (DoughnutChart)LoadControl("~/UserControls/DoughnutChart.ascx");
                    subjectChart.NumCorrect = correctResponses;
                    subjectChart.NumIncorrect = totalResponses - correctResponses;
                    subjectChart.Chart.DataBind();
                    pnlTest.Controls.Add(subjectChart);
                }
                else
                {
                    DoughnutChart subjectChart = (DoughnutChart)LoadControl("~/UserControls/DoughnutChart.ascx");
                    string subjectName = ddlFilterSubject.Text;
                    IEnumerable<StudentSubjectRollingStats> studentAllSubjectRollingStats = await student.StudentSubjectRollingStats.FetchAllIfNeededAsync();
                    StudentSubjectRollingStats studentSubjectRollingStats = studentAllSubjectRollingStats.Single(x => x.Subject == subjectName);
                    
                    int totalResponses =  0;
                    int correctResponses = 0;
                    if(ddlFilterTime.SelectedValue == "PastWeek") {
                        totalResponses =  studentSubjectRollingStats.TotalPastWeek;
                        correctResponses = studentSubjectRollingStats.CorrectPastWeek;
                    }
                    else if(ddlFilterTime.SelectedValue == "PastMonth") {
                        totalResponses =  studentSubjectRollingStats.TotalPastMonth;
                        correctResponses = studentSubjectRollingStats.CorrectPastMonth;
                    }
                    else {
                        totalResponses =  studentSubjectRollingStats.TotalAllTime;
                        correctResponses = studentSubjectRollingStats.CorrectAllTime;
                    }
                    subjectChart.NumCorrect = correctResponses;
                    subjectChart.NumIncorrect = totalResponses - correctResponses;
                    subjectChart.Chart.DataBind();
                    pnlTest.Controls.Add(subjectChart);

                    string[] categories = Constants.SubjectToCategory[ddlFilterSubject.Text];
                    IEnumerable<StudentCategoryRollingStats> studentAllCategoryRollingStats = await student.StudentCategoryRollingStats.FetchAllIfNeededAsync();
                    int i = 1;
                    foreach (string catName in categories)
                    {
                        SingleBarChart catChart = (SingleBarChart)LoadControl("~/UserControls/SingleBarChart.ascx");
                        StudentCategoryRollingStats categoryStats = studentAllCategoryRollingStats.First(x => x.Category == catName);
                        
                        if(ddlFilterTime.SelectedValue == "PastWeek") {
                            totalResponses =  categoryStats.TotalPastWeek;
                            correctResponses = categoryStats.CorrectPastWeek;
                        }
                        else if(ddlFilterTime.SelectedValue == "PastMonth") {
                            totalResponses =  categoryStats.TotalPastMonth;
                            correctResponses = categoryStats.CorrectPastMonth;
                        }
                        else {
                            totalResponses =  categoryStats.TotalAllTime;
                            correctResponses = categoryStats.CorrectAllTime;
                        }
                        catChart.SetCorrectIncorrect(correctResponses, totalResponses - correctResponses);
                        catChart.Chart.DataBind();
                        pnlTest.Controls.Add(catChart);
                    }
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

        protected void btnUpdatePanels_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildCharts().Wait();
        }
    }
}