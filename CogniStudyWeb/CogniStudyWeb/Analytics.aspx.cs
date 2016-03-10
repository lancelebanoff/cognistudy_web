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
using System.Diagnostics;
using CogniStudyWeb.UserControls;

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
                // ALL STUDENTS/SUBJECTS
                if (ddlFilterSubject.Text == "All Subjects")
                {
                    int totalResponses = 0;
                    int correctResponses = 0;
                    IList<ParseObject> subjectStats = await ParseCloud.CallFunctionAsync<IList<ParseObject>>("getAllSubStats", null);
                    foreach (ParseObject stat in subjectStats)
                    {
                        totalResponses += stat.Get<int>("totalResponses");
                        correctResponses += stat.Get<int>("correctResponses");
                    }
                    DoughnutChart subjectChart = (DoughnutChart)LoadControl("~/UserControls/DoughnutChart.ascx");
                    subjectChart.NumCorrect = correctResponses;
                    subjectChart.NumIncorrect = totalResponses - correctResponses;
                    subjectChart.Title = ddlFilterSubject.Text;
                    subjectChart.Chart.DataBind();
                    pnlTest.Controls.Add(subjectChart);
                }
                // ALL STUDENTS, one subject
                else
                {
                    DoughnutChart subjectChart = (DoughnutChart)LoadControl("~/UserControls/DoughnutChart.ascx");
                    IDictionary<string, object> parameters = new Dictionary<string, object>
                    {
                        { "subjectName", ddlFilterSubject.Text }
                    };
                    ParseObject subjectStats = await ParseCloud.CallFunctionAsync<ParseObject>("getSubStats", parameters);
                    subjectChart.NumCorrect = subjectStats.Get<int>("correctResponses");
                    subjectChart.NumIncorrect = subjectStats.Get<int>("totalResponses") - subjectStats.Get<int>("correctResponses");
                    subjectChart.Title = subjectStats.Get<string>("subject");
                    subjectChart.Chart.DataBind();
                    pnlTest.Controls.Add(subjectChart);

                    string[] categories = Constants.SubjectToCategory[ddlFilterSubject.Text];
                    IDictionary<string, object> catParams = new Dictionary<string, object>
                    {
                        { "catNames", categories }
                    };
                    IList<ParseObject> categoryStats = await ParseCloud.CallFunctionAsync<IList<ParseObject>>("getSomeCatStats", catParams);
                    foreach (ParseObject category in categoryStats)
                    {
                        SingleBarChart catChart = (SingleBarChart)LoadControl("~/UserControls/SingleBarChart.ascx");
                        catChart.SetCorrectIncorrect(category.Get<int>("correctResponses"), category.Get<int>("totalResponses") - category.Get<int>("correctResponses"));
                        catChart.Title = category.Get<string>("category");
                        catChart.Chart.DataBind();
                        pnlTest.Controls.Add(catChart);
                    }
                }
            }
            // one student
            else
            {
                PublicUserData publicUserData = await PublicUserData.GetById(ddlFilterStudent.SelectedValue);
                Student student = await publicUserData.Student.FetchIfNeededAsync();
                //PrivateStudentData privateStudentData = await student.PrivateStudentData.FetchIfNeededAsync();

                // one student, ALL SUBJECTS
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
                    Debug.WriteLine(totalResponses + " " + correctResponses);
                    DoughnutChart subjectChart = (DoughnutChart)LoadControl("~/UserControls/DoughnutChart.ascx");
                    subjectChart.NumCorrect = correctResponses;
                    subjectChart.NumIncorrect = totalResponses - correctResponses;
                    subjectChart.Title = ddlFilterSubject.Text;
                    subjectChart.Chart.DataBind();
                    pnlTest.Controls.Add(subjectChart);

                    ProgressBarChart progressChart = (ProgressBarChart)LoadControl("~/UserControls/ProgressBarChart.ascx");
                    ParseRelation<ParseObject> rel = student.GetRelation<ParseObject>("studentTotalDayStats");
                    int startBlockNum = 54;
                    DateTime startDate = DateTime.Today.AddDays(-10);
                    int endBlockNum = 64;
                    List<ParseObject> stats = new List<ParseObject>();
                    for(int i = startBlockNum; i <= endBlockNum; i++) {
                        var query = from s in rel.Query
                                    where s.Get<int>("blockNum") == i
                                    select s;
                        ParseObject stat = await query.FirstOrDefaultAsync();
                        stats.Add(stat);
                    }
                    progressChart.SetUp(stats, startDate);
                    pnlTest.Controls.Add(progressChart);
                }
                // one student, one subject
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
                    subjectChart.Title = subjectName;
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
                        catChart.Title = catName;
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

        protected void btnUpdatePanels_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(BuildCharts));
        }
    }
}