using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace WebMining
{
    public partial class GraphForm : Form
    {
        static int id = 1;
        public GraphForm()
        {
            InitializeComponent();

            Text = "GraphForm " + id++;
        }

        public GraphForm CloneMe()
        {
            return new GraphForm().AddGraphs(g1, g2);
        }

        Graph g1, g2;
        public GraphForm AddGraphs(Graph graph1, Graph graph2)
        {
            g1 = graph1;
            g2 = graph2;

            panel1.Controls.Add(new GViewer() {
                Graph = graph1,
                Dock = DockStyle.Fill,
                LayoutAlgorithmSettingsButtonVisible = false,
                EdgeInsertButtonVisible = false,
                UndoRedoButtonsVisible = false,
            });

            panel2.Controls.Add(new GViewer() {
                Graph = graph2,
                Dock = DockStyle.Fill,
                LayoutAlgorithmSettingsButtonVisible = false,
                EdgeInsertButtonVisible = false,
                UndoRedoButtonsVisible = false,
            });
            return this;
        }
    }
}
