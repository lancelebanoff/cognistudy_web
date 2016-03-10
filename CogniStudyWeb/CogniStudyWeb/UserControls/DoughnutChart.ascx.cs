using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace CogniTutor.UserControls
{
    public partial class DoughnutChart : System.Web.UI.UserControl
    {
        public Chart Chart { get { return Chart1; } }
        public double NumCorrect { set { Chart.Series[0].Points[0].YValues[0] = value; } }
        public double NumIncorrect { set { Chart.Series[0].Points[1].YValues[0] = value; } }
        public string Title { set { Chart.Titles.Clear(); Chart.Titles.Add(value); } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}