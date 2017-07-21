using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WebMining
{
    public partial class ChartForm : Form
    {
        static int id = 0;
        public ChartForm()
        {
            id++;
            InitializeComponent();

            Text = "ChartForm " + id;
        }

        public ChartForm CloneMe()
        {
            return new ChartForm().SetChart(c, b, o, g, p, p2);
        }

        Series c, b, o, g, p, p2;
        public ChartForm SetChart(Series country, Series browser, Series os, Series gender, Series pages, Series pages2)
        {
            c = country;
            b = browser;
            o = os;
            g = gender;
            p = pages;
            p2 = pages2;

            countryChart.Series.Clear();
            countryChart.Series.Add(country);

            browserChart.Series.Clear();
            browserChart.Series.Add(browser);

            osChart.Series.Clear();
            osChart.Series.Add(os);

            genderChart.Series.Clear();
            genderChart.Series.Add(gender);

            pagesChart.Series.Clear();

            pagesChart.Series.Add(pages2);
            pagesChart.Series.Add(pages);

            return this;
        }
    }
}
