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

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void SetUp(List<ParseObject> Stats, DateTime startDate)
        {
            Chart.Series["Series1"].Points.Clear();
            Chart.Series["Series2"].Points.Clear();
            //IEnumerable<ParseObject> stats = Stats.OrderBy(s => s.Get<int>("blockNum"));
            DateTime curDate = startDate;
            foreach (ParseObject stat in Stats)
            {
                DataPoint p = new DataPoint();
                DataPoint q = new DataPoint();
                p.YValues = new double[] { stat == null ? 0 : stat.Get<int>("correct") };
                q.YValues = new double[] { stat == null ? 0 : stat.Get<int>("total") - stat.Get<int>("correct") };
                p.AxisLabel = curDate.ToShortDateString();
                q.AxisLabel = curDate.ToShortDateString();
                Chart.Series["Series1"].Points.Add(p);
                Chart.Series["Series2"].Points.Add(q);
                curDate = curDate.AddDays(1);
            }
        }
    }
}