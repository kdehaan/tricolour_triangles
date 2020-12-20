

namespace TricolourTriangles
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class GraphDrawer
    {
        private static readonly Dictionary<Colour, Microsoft.Msagl.Drawing.Color> ColourReference
        = new Dictionary<Colour, Microsoft.Msagl.Drawing.Color>
        {
            { Colour.Red, Microsoft.Msagl.Drawing.Color.Red },
            { Colour.Green, Microsoft.Msagl.Drawing.Color.Green },
            { Colour.Blue, Microsoft.Msagl.Drawing.Color.Blue },
        };

        private System.Windows.Forms.Form form = new System.Windows.Forms.Form();
        private Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        private Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        private int nextNodeName = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphDrawer"/> class.
        /// </summary>
        /// <param name="polygon"></param>
        public GraphDrawer(Polygon polygon)
        {

            string lastNodeName = null;
            string firstNodeName = null;

            foreach (Colour borderColour in polygon.GetBorder())
            {
                string nodeName = this.GetNodeName();
                this.CreateNode(nodeName, borderColour);

                if (lastNodeName != null)
                {
                    this.CreateEdge(lastNodeName, nodeName);
                } else
                {
                    firstNodeName = nodeName;
                }

                lastNodeName = nodeName;
            }
            this.CreateEdge(lastNodeName, firstNodeName);
        }

        public void DrawGraph()
        {
            this.viewer.Graph = this.graph;
            this.form.SuspendLayout();
            this.viewer.Dock = DockStyle.Fill;
            this.form.Controls.Add(this.viewer);
            this.form.ResumeLayout();
            this.form.ShowDialog();
        }

        private void CreateEdge(string a, string b)
        {
            Microsoft.Msagl.Drawing.Edge edge = this.graph.AddEdge(a, b);
            edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
        }

        private void CreateNode(string nodeName, Colour nodeColour)
        {
            Microsoft.Msagl.Drawing.Node node = this.graph.AddNode(nodeName);
            node.Attr.Color = ColourReference[nodeColour];
            node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
        }




        private string GetNodeName()
        {
            return this.nextNodeName++.ToString();
        }
    }


}
