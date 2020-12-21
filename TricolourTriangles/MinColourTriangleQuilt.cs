// <copyright file="MinColourTriangleQuilt.cs" company="Kevin de Haan (github.com/kdehaan)">
// Written by Kevin de Haan (github.com/kdehaan)
// </copyright>

namespace TricolourTriangles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Creates a 'quilt' (graph) of triangles such that there is a minimal number of 'patches' with three colours of nodes.
    /// </summary>
    public class MinColourTriangleQuilt : IQuilt
    {
        private GraphDrawer graphDrawer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinColourTriangleQuilt"/> class with random border nodes.
        /// </summary>
        /// <param name="perimeterLength">The length of the random perimeter.</param>
        /// <param name="drawGraph">Option to disable graph creation.</param>
        public MinColourTriangleQuilt(int perimeterLength, bool drawGraph = true)
            : this(CreateRandomBorder(perimeterLength), drawGraph)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinColourTriangleQuilt"/> class.
        /// </summary>
        /// <param name="border">A list of nodes to use as a border.</param>
        /// <param name="drawGraph">Option to disable graph creation.</param>
        public MinColourTriangleQuilt(List<QuiltNode> border, bool drawGraph = true)
        {
            this.Border = border;
            if (drawGraph)
            {
                this.graphDrawer = new GraphDrawer(this.Border);
            }

            this.FindQuiltPatches(drawGraph);
        }

        /// <summary>
        /// Gets the list of nodes representing the outermost perimeter of the quilt.
        /// </summary>
        public List<QuiltNode> Border { get; private set; }

        /// <summary>
        /// Gets the minimum number of three colour triangles possible for this quilt.
        /// </summary>
        public int NumTricolourTriangles { get; private set; }

        /// <summary>
        /// Draws the quilt as a graph.
        /// </summary>
        public void Visualise()
        {
            this.graphDrawer.DrawGraph();
        }

        private static List<QuiltNode> CreateRandomBorder(int perimeterLength)
        {
            Random random = new Random(0);
            if (perimeterLength < 3)
            {
                throw new ArgumentException("Parameter cannot be less than 3", nameof(perimeterLength));
            }

            Array colourTypes = Enum.GetValues(typeof(Colour));
            return Enumerable
                .Range(0, perimeterLength)
                .Select(i => new QuiltNode(i, (Colour)colourTypes.GetValue(random.Next(colourTypes.Length))))
                .ToList();
        }

        private static bool OppositeEdges((Colour, Colour) a, (Colour, Colour) b)
        {
            if (a.Item1 == b.Item2 && a.Item2 == b.Item1)
            {
                return true;
            }

            return false;
        }

        private static int WrapIndex(int index, int listCount)
        {
            int remainder = index % listCount;
            return remainder < 0 ? remainder + listCount : remainder;
        }

        private static List<QuiltNode> WrapRange(List<QuiltNode> list, int start, int end)
        {
            start = WrapIndex(start, list.Count);
            end = WrapIndex(end, list.Count);
            if (start > end)
            {
                List<QuiltNode> subList = list.GetRange(0, end + 1).Concat(list.GetRange(start, list.Count - start)).ToList();
                return subList;
            }

            return list.GetRange(start, (end - start) + 1);
        }

        private void FindQuiltPatches(bool drawGraph)
        {
            int nextNodeId = this.Border.Count;
            List<QuiltNode> activeBorder = new List<QuiltNode>(this.Border); // active nodes under consideration
            List<QuiltEdge> edges = new List<QuiltEdge> // list of edges that can potentially 'negate' each other
            {
                new QuiltEdge((activeBorder.LastOrDefault().Type, activeBorder.FirstOrDefault().Type), 0),
            };

            int borderIndex = 1;
            while (borderIndex < activeBorder.Count)
            {
                (Colour, Colour) edgeType = (activeBorder[borderIndex - 1].Type, activeBorder[borderIndex].Type);

                // edge has a change in colour
                if (edgeType.Item1 != edgeType.Item2)
                {
                    // the colour change is opposite the previous edge
                    if (OppositeEdges(edgeType, edges.LastOrDefault().Type))
                    {
                        int startIndex = WrapIndex(edges.LastOrDefault().Index - 1, activeBorder.Count);

                        // create and connect a new node to the 'palindrome' nodes
                        QuiltNode newNode = new QuiltNode(nextNodeId++, edgeType.Item2);
                        if (drawGraph)
                        {
                            this.graphDrawer.JoinNode(newNode, WrapRange(activeBorder, startIndex, borderIndex));
                        }

                        // remove the nodes from the center of the 'palindrome'
                        int nextIndex = WrapIndex(startIndex + 1, activeBorder.Count);
                        foreach (QuiltNode node in WrapRange(activeBorder, startIndex + 1, borderIndex - 1))
                        {
                            activeBorder.Remove(node);
                        }

                        // replace the removed nodes with the new active node
                        activeBorder.Insert(WrapIndex(edges.LastOrDefault().Index, activeBorder.Count), newNode);
                        borderIndex = nextIndex;

                        // pop the edge stack
                        edges.RemoveAt(edges.Count - 1);
                    }
                    else
                    {
                        edges.Add(new QuiltEdge(edgeType, borderIndex));
                    }
                }

                borderIndex++;
            }

            // final node connects to all active nodes, colour does not matter
            QuiltNode capstoneNode = new QuiltNode(nextNodeId++, Colour.Red);
            if (drawGraph)
            {
                this.graphDrawer.JoinNode(capstoneNode, activeBorder, true);
            }

            this.NumTricolourTriangles = edges.Count / 3;
        }
    }
}
