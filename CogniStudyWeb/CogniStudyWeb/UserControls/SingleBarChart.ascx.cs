﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace CogniTutor.UserControls
{
    public partial class SingleBarChart : System.Web.UI.UserControl
    {
        public Chart Chart { get { return Chart2; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetCorrectIncorrect(int correct, int incorrect)
        {
            Chart.Series["Series1"].Points.AddY(correct);
            Chart.Series["Series2"].Points.AddY(incorrect);
        }
    }
}