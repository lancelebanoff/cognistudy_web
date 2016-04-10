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
using CogniStudyWeb;

namespace CogniTutor
{
    public partial class Analytics : CogniPage
    {
        UpdatePanel[] updatePanels;
        UpdatePanel[] catUpdatePanels;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override async Task OnStart()
        {
            updatePanels = new UpdatePanel[] {UpdatePanel1, UpdatePanel2, UpdatePanel3, UpdatePanel4, 
                    UpdatePanel5, UpdatePanel6, UpdatePanel7, UpdatePanel8, UpdatePanel9, UpdatePanel10};
            catUpdatePanels = new UpdatePanel[] {catUpdatePanel1, catUpdatePanel2, catUpdatePanel3, catUpdatePanel4, 
                    catUpdatePanel5, catUpdatePanel6, catUpdatePanel7, catUpdatePanel8, catUpdatePanel9, catUpdatePanel10};
            if (!IsPostBack)
            {
                await FillFilterDropdowns();
            }
            if(!IsPostBack)
            {
                await BuildCharts();
            }
        }

        private async Task FillFilterDropdowns()
        {
            ddlFilterSubject.DataSource = Constants.GetPublicStringProperties(typeof(Constants.Subject));
            ddlFilterSubject.DataBind();
            ddlFilterSubject.Items.Insert(0, "All Subjects");
            ddlFilterCategory.Items.Insert(0, "All Categories");
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
            ClearCharts();
            int updatePanelCounter = 0;
            int catUpdatePanelCounter = 0;
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
                    updatePanels[updatePanelCounter++].ContentTemplateContainer.Controls.Add(subjectChart);
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
                    updatePanels[updatePanelCounter++].ContentTemplateContainer.Controls.Add(subjectChart);

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
                        catUpdatePanels[catUpdatePanelCounter++].ContentTemplateContainer.Controls.Add(catChart);
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
                    updatePanels[updatePanelCounter++].ContentTemplateContainer.Controls.Add(subjectChart);
                }
                // one student, one subject
                else
                {
                    // ALL CATEGORIES
                    if (ddlFilterCategory.Text == "All Categories")
                    {
                        DoughnutChart subjectChart = (DoughnutChart)LoadControl("~/UserControls/DoughnutChart.ascx");
                        string subjectName = ddlFilterSubject.Text;
                        IEnumerable<StudentSubjectRollingStats> studentAllSubjectRollingStats = await student.StudentSubjectRollingStats.FetchAllIfNeededAsync();
                        StudentSubjectRollingStats studentSubjectRollingStats = studentAllSubjectRollingStats.Single(x => x.Subject == subjectName);

                        int totalResponses = 0;
                        int correctResponses = 0;
                        if (ddlFilterTime.SelectedValue == "PastWeek")
                        {
                            totalResponses = studentSubjectRollingStats.TotalPastWeek;
                            correctResponses = studentSubjectRollingStats.CorrectPastWeek;
                        }
                        else if (ddlFilterTime.SelectedValue == "PastMonth")
                        {
                            totalResponses = studentSubjectRollingStats.TotalPastMonth;
                            correctResponses = studentSubjectRollingStats.CorrectPastMonth;
                        }
                        else
                        {
                            totalResponses = studentSubjectRollingStats.TotalAllTime;
                            correctResponses = studentSubjectRollingStats.CorrectAllTime;
                        }
                        subjectChart.NumCorrect = correctResponses;
                        subjectChart.NumIncorrect = totalResponses - correctResponses;
                        subjectChart.Title = subjectName;
                        subjectChart.Chart.DataBind();
                        updatePanels[updatePanelCounter++].ContentTemplateContainer.Controls.Add(subjectChart);

                        string[] categories = Constants.SubjectToCategory[ddlFilterSubject.Text];
                        IEnumerable<StudentCategoryRollingStats> studentAllCategoryRollingStats = await student.StudentCategoryRollingStats.FetchAllIfNeededAsync();
                        //int i = 1;
                        foreach (string catName in categories)
                        {
                            SingleBarChart catChart = (SingleBarChart)LoadControl("~/UserControls/SingleBarChart.ascx");
                            StudentCategoryRollingStats categoryStats = studentAllCategoryRollingStats.First(x => x.Category == catName);

                            if (ddlFilterTime.SelectedValue == "PastWeek")
                            {
                                totalResponses = categoryStats.TotalPastWeek;
                                correctResponses = categoryStats.CorrectPastWeek;
                            }
                            else if (ddlFilterTime.SelectedValue == "PastMonth")
                            {
                                totalResponses = categoryStats.TotalPastMonth;
                                correctResponses = categoryStats.CorrectPastMonth;
                            }
                            else
                            {
                                totalResponses = categoryStats.TotalAllTime;
                                correctResponses = categoryStats.CorrectAllTime;
                            }
                            catChart.SetCorrectIncorrect(correctResponses, totalResponses - correctResponses);
                            catChart.Title = catName;
                            catChart.Chart.DataBind();
                            catUpdatePanels[catUpdatePanelCounter++].ContentTemplateContainer.Controls.Add(catChart);
                        }
                    }
                    // one category
                    else
                    {
                        DoughnutChart categoryChart = (DoughnutChart)LoadControl("~/UserControls/DoughnutChart.ascx");
                        string categoryName = ddlFilterCategory.Text;
                        IEnumerable<StudentCategoryRollingStats> studentAllCategoryRollingStats = await student.StudentCategoryRollingStats.FetchAllIfNeededAsync();
                        StudentCategoryRollingStats studentCategoryRollingStats = studentAllCategoryRollingStats.Single(x => x.Category == categoryName);

                        int totalResponses = 0;
                        int correctResponses = 0;
                        if (ddlFilterTime.SelectedValue == "PastWeek")
                        {
                            totalResponses = studentCategoryRollingStats.TotalPastWeek;
                            correctResponses = studentCategoryRollingStats.CorrectPastWeek;
                        }
                        else if (ddlFilterTime.SelectedValue == "PastMonth")
                        {
                            totalResponses = studentCategoryRollingStats.TotalPastMonth;
                            correctResponses = studentCategoryRollingStats.CorrectPastMonth;
                        }
                        else
                        {
                            totalResponses = studentCategoryRollingStats.TotalAllTime;
                            correctResponses = studentCategoryRollingStats.CorrectAllTime;
                        }
                        categoryChart.NumCorrect = correctResponses;
                        categoryChart.NumIncorrect = totalResponses - correctResponses;
                        categoryChart.Title = categoryName;
                        categoryChart.Chart.DataBind();
                        updatePanels[updatePanelCounter++].ContentTemplateContainer.Controls.Add(categoryChart);

                    }
                }

                ProgressBarChart progressChart = (ProgressBarChart)LoadControl("~/UserControls/ProgressBarChart.ascx");
                DateTime curDate;
                int startBlockNum;
                int endBlockNum;
                ParseRelation<ParseObject> rel;
                List<DataPoint> correctDataPoints = new List<DataPoint>();
                List<DataPoint> incorrectDataPoints = new List<DataPoint>();
                string blockStatsType = FigureBlockStatsType();
                string blockStatsSelection = FigureBlockStatsSelection();
                if (ddlFilterTime.SelectedValue == "PastWeek")
                {
                    rel = student.GetRelation<ParseObject>("student" + blockStatsType + "DayStats");
                    endBlockNum = DateUtils.getCurrentDayBlockNum();
                    startBlockNum = endBlockNum - 6;
                    curDate = DateTime.Today.AddDays(-6);

                    for (int i = startBlockNum; i <= endBlockNum; i++, curDate = curDate.AddDays(1))
                    {
                        var query = from s in rel.Query
                                    where s.Get<int>("blockNum") == i
                                    select s;
                        if (blockStatsType != "Total")
                        {
                            query = from s in rel.Query
                                    where s.Get<int>("blockNum") == i
                                    where s.Get<string>(blockStatsType.ToLower()) == blockStatsSelection
                                    select s;
                        }
                        ParseObject stat = await query.FirstOrDefaultAsync();
                        DataPoint correct = new DataPoint();
                        DataPoint incorrect = new DataPoint();
                        correct.YValues = new double[] { stat == null ? 0 : stat.Get<int>("correct") };
                        incorrect.YValues = new double[] { stat == null ? 0 : stat.Get<int>("total") - stat.Get<int>("correct") };
                        correct.AxisLabel = curDate.ToShortDateString();
                        incorrect.AxisLabel = curDate.ToShortDateString();
                        correctDataPoints.Add(correct);
                        incorrectDataPoints.Add(incorrect);
                    }
                }
                else if (ddlFilterTime.SelectedValue == "PastMonth")
                {
                    rel = student.GetRelation<ParseObject>("student" + blockStatsType + "DayStats");
                    endBlockNum = DateUtils.getCurrentDayBlockNum();
                    startBlockNum = endBlockNum - 29;
                    curDate = DateTime.Today.AddDays(-29);

                    for (int i = startBlockNum; i <= endBlockNum; i += 3, curDate = curDate.AddDays(3))
                    {
                        var query = from s in rel.Query
                                    where s.Get<int>("blockNum") >= i
                                    where s.Get<int>("blockNum") <= i + 2
                                    select s;
                        if (blockStatsType != "Total")
                        {
                            query = from s in rel.Query
                                    where s.Get<int>("blockNum") == i
                                    where s.Get<string>(blockStatsType.ToLower()) == blockStatsSelection
                                    select s;
                        }
                        IEnumerable<ParseObject> stats = await query.FindAsync();
                        int totalCorrect = 0, totalIncorrect = 0;
                        foreach (ParseObject stat in stats)
                        {
                            totalCorrect += stat.Get<int>("correct");
                            totalIncorrect += stat.Get<int>("total") - stat.Get<int>("correct");
                        }
                        DataPoint correct = new DataPoint();
                        DataPoint incorrect = new DataPoint();
                        correct.YValues = new double[] { totalCorrect };
                        incorrect.YValues = new double[] { totalIncorrect };
                        correct.AxisLabel = curDate.ToShortDateString();
                        incorrect.AxisLabel = curDate.ToShortDateString();
                        correctDataPoints.Add(correct);
                        incorrectDataPoints.Add(incorrect);
                    }
                }
                else
                {
                    rel = student.GetRelation<ParseObject>("student" + blockStatsType + "MonthStats");
                    endBlockNum = DateUtils.getCurrentMonthBlockNum();
                    startBlockNum = endBlockNum - 11;
                    curDate = DateTime.Today.AddMonths(-11);

                    for (int i = startBlockNum; i <= endBlockNum; i++, curDate = curDate.AddMonths(1))
                    {
                        var query = from s in rel.Query
                                    where s.Get<int>("blockNum") == i
                                    select s;
                        if (blockStatsType != "Total")
                        {
                            query = from s in rel.Query
                                    where s.Get<int>("blockNum") == i
                                    where s.Get<string>(blockStatsType.ToLower()) == blockStatsSelection
                                    select s;
                        }
                        ParseObject stat = await query.FirstOrDefaultAsync();
                        DataPoint correct = new DataPoint();
                        DataPoint incorrect = new DataPoint();
                        correct.YValues = new double[] { stat == null ? 0 : stat.Get<int>("correct") };
                        incorrect.YValues = new double[] { stat == null ? 0 : stat.Get<int>("total") - stat.Get<int>("correct") };
                        correct.AxisLabel = curDate.ToShortDateString();
                        incorrect.AxisLabel = curDate.ToShortDateString();
                        correctDataPoints.Add(correct);
                        incorrectDataPoints.Add(incorrect);
                    }
                }
                progressChart.SetUp(correctDataPoints, incorrectDataPoints);
                progressChart.Title = publicUserData.DisplayName + "'s Progress in " + FigureMostSpecificType();
                if (blockStatsType == "Subject")
                    updatePanels[updatePanelCounter++].ContentTemplateContainer.Controls.Add(progressChart);
                else
                    catUpdatePanels[catUpdatePanelCounter++].ContentTemplateContainer.Controls.Add(progressChart);
            }
        }

        private string FigureBlockStatsSelection()
        {
            if (ddlFilterCategory.Text != "All Categories")
                return ddlFilterCategory.Text;
            else if (ddlFilterSubject.Text != "All Subjects")
                return ddlFilterSubject.Text;
            else
                throw new Exception("Total block stats should not have a selection");
        }

        private string FigureBlockStatsType()
        {
            if (ddlFilterCategory.Text != "All Categories")
                return "Category";
            else if (ddlFilterSubject.Text != "All Subjects")
                return "Subject";
            else
                return "";
        }

        private string FigureMostSpecificType()
        {
            if (ddlFilterCategory.Text != "All Categories")
                return ddlFilterCategory.Text;
            else
                return ddlFilterSubject.Text;
        }

        private void ClearCharts()
        {
            foreach (UpdatePanel pnl in updatePanels)
                pnl.ContentTemplateContainer.Controls.Clear();
            foreach (UpdatePanel pnl in catUpdatePanels)
                pnl.ContentTemplateContainer.Controls.Clear();
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
            if (ddlFilterSubject.Text == "All Subjects" || ddlFilterStudent.Text == "All Students")
            {
                ddlFilterCategory.DataSource = null;
                ddlFilterCategory.DataBind();
                ddlFilterCategory.Items.Insert(0, "All Categories");
                ddlFilterCategory.Enabled = false;
            }
            else if(sender == ddlFilterSubject)
            {
                ddlFilterCategory.DataSource = Constants.SubjectToCategory[ddlFilterSubject.Text];
                ddlFilterCategory.DataBind();
                ddlFilterCategory.Items.Insert(0, "All Categories");
                ddlFilterCategory.Enabled = true;
            }
            RegisterAsyncTask(new PageAsyncTask(BuildCharts));
        }
    }
}