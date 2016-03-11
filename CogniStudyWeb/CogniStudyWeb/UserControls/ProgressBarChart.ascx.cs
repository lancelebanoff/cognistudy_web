using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace CogniStudyWeb.UserControls
{
    public partial class ProgressBarChart : System.Web.UI.UserControl
    {
        public Chart Chart { get { return Chart1; } }
        public string Title { set { Chart.Titles[0].Text = value; Chart.Titles[0].Font = new System.Drawing.Font("Trebuchet MS", 12); } }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void SetUp(List<DataPoint> correctDataPoints, List<DataPoint> incorrectDataPoints)
        {
            Chart.Series["Series1"].Points.Clear();
            Chart.Series["Series2"].Points.Clear();
            foreach (DataPoint p in correctDataPoints)
            {
                //DataPoint p = new DataPoint();
                //DataPoint q = new DataPoint();
                //p.YValues = new double[] { stat == null ? 0 : stat.Get<int>("correct") };
                //q.YValues = new double[] { stat == null ? 0 : stat.Get<int>("total") - stat.Get<int>("correct") };
                //p.AxisLabel = curDate.ToShortDateString();
                //q.AxisLabel = curDate.ToShortDateString();
                Chart.Series["Series1"].Points.Add(p);
                //if (timeFrame == "PastWeek")
                //{
                //    curDate = curDate.AddDays(1);
                //}
                //else if (timeFrame == "PastMonth")
                //{
                //    curDate = curDate.AddDays(3);
                //}
                //else
                //{
                //    curDate = curDate.AddMonths(1);
                //}
            }
            foreach (DataPoint q in incorrectDataPoints)
            {
                Chart.Series["Series2"].Points.Add(q);
            }
        }
    }
}