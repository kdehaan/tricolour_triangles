// <copyright file="GraphDrawer.cs" company="Kevin de Haan (github.com/kdehaan)">
// Written by Kevin de Haan (github.com/kdehaan)
// </copyright>

namespace TricolourTriangles
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Microsoft.Msagl.Core.Geometry;
    using Microsoft.Msagl.Core.Geometry.Curves;
    using Microsoft.Msagl.Core.Layout;
    using Microsoft.Msagl.Core.Routing;
    using Microsoft.Msagl.GraphViewerGdi;
    using Microsoft.Msagl.Layout.Layered;

    /// <summary>
    /// Used to maintain and visualize a representation of an evolving Polygon Graph.
    /// </summary>
    public class GraphDrawer
    {
        private static readonly Dictionary<Colour, Microsoft.Msagl.Drawing.Color> ColourReference
        = new Dictionary<Colour, Microsoft.Msagl.Drawing.Color>
        {
            { Colour.Red, Microsoft.Msagl.Drawing.Color.Red },
            { Colour.Green, Microsoft.Msagl.Drawing.Color.Green },
            { Colour.Blue, Microsoft.Msagl.Drawing.Color.Blue },
        };

        private readonly Form form = new Form();
        private readonly GViewer viewer = new GViewer();
        private readonly Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphDrawer"/> class.
        /// </summary>
        /// <param name="border">Inital node perimeter.</param>
        public GraphDrawer(List<QuiltNode> border)
        {
            QuiltNode lastNode = new QuiltNode(-1, Colour.Red);
            QuiltNode firstNode = new QuiltNode(-1, Colour.Red);

            // Note: structs are copied on assignment
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct
            foreach (QuiltNode node in border)
            {
                this.CreateNode(node);

                if (lastNode.Id == -1)
                {
                    firstNode = node;
                }
                else
                {
                    this.CreateEdge(lastNode, node);
                }

                lastNode = node;
            }

            this.CreateEdge(lastNode, firstNode);
            this.CreateAndLayoutGraph(border);
        }

        private void CreateAndLayoutGraph(List<QuiltNode> border)
        {
            this.graph.CreateGeometryGraph();
            foreach (QuiltNode node in border)
            {
                this.addGeometryNode(node.Id);
            }
            var settings = new SugiyamaLayoutSettings();
            //{
            //    Transformation = PlaneTransformation.Rotation(Math.PI / 2),
            //    EdgeRoutingSettings = { EdgeRoutingMode = EdgeRoutingMode.Spline },
            //};

            var layout = new LayeredLayout(this.graph.GeometryGraph, settings);

            layout.Run();
        }

        /// <summary>
        /// Produces a visualization of the current GraphDrawer object.
        /// </summary>
        public void DrawGraph()
        {
            this.viewer.Graph = this.graph;
            this.form.SuspendLayout();
            this.viewer.Dock = DockStyle.Fill;
            this.form.Controls.Add(this.viewer);
            this.form.ResumeLayout();
            this.form.ShowDialog();
        }

        /// <summary>
        /// Creates edges between the primary node and the list of other nodes.
        /// </summary>
        /// <param name="node">Primary Node.</param>
        /// <param name="activeNodes">Nodes to connect to.</param>
        /// <param name="emphasis">Highlight the created node.</param>
        public void JoinNode(QuiltNode node, List<QuiltNode> activeNodes, bool emphasis = false)
        {
            this.CreateNode(node, emphasis);
            foreach (QuiltNode activeNode in activeNodes)
            {
                this.CreateEdge(node, activeNode);
            }
        }

        private void CreateEdge(QuiltNode a, QuiltNode b)
        {
            Microsoft.Msagl.Drawing.Edge edge = this.graph.AddEdge(a.Id.ToString(), b.Id.ToString());
            edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
        }

        private void CreateNode(QuiltNode quiltNode, bool emphasis = false)
        {
            Microsoft.Msagl.Drawing.Node node = this.graph.AddNode(quiltNode.Id.ToString());
            node.Attr.Color = ColourReference[quiltNode.Type];
            node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
            if (emphasis)
            {
                node.Attr.FillColor = ColourReference[quiltNode.Type];
            }
        }

        private void addGeometryNode(int id)
        {
            this.graph.GeometryGraph.Nodes.Add(new Node(CreateCurve(40, 40), id.ToString()));
        }
        private static ICurve CreateCurve(double w, double h)
        {
            return CurveFactory.CreateRectangle(w, h, default(Point));
        }
    }
}
