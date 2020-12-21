// <copyright file="Quilt.cs" company="Kevin de Haan (github.com/kdehaan)">
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
    public class Quilt
    {
        private GraphDrawer graphDrawer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Quilt"/> class with random border nodes.
        /// </summary>
        /// <param name="perimeterLength">The length of the random perimeter.</param>
        /// <param name="drawGraph">Option to disable graph creation.</param>
        public Quilt(int perimeterLength, bool drawGraph = true)
            : this(CreateRandomBorder(perimeterLength), drawGraph)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quilt"/> class.
        /// </summary>
        /// <param name="border">A list of nodes to use as a border.</param>
        /// <param name="drawGraph">Option to disable graph creation.</param>
        public Quilt(List<QuiltNode> border, bool drawGraph = true)
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
            List<QuiltNode> activeBorder = new List<QuiltNode>(this.Border);
            List<QuiltEdge> edges = new List<QuiltEdge>
            {
                new QuiltEdge((activeBorder.LastOrDefault().Type, activeBorder.FirstOrDefault().Type), 0),
            };

            int index = 1;
            while (index < activeBorder.Count)
            {
                (Colour, Colour) edgeType = (activeBorder[index - 1].Type, activeBorder[index].Type);
                if (edgeType.Item1 != edgeType.Item2)
                {
                    if (OppositeEdges(edgeType, edges.LastOrDefault().Type))
                    {
                        int startIndex = WrapIndex(edges.LastOrDefault().Index - 1, activeBorder.Count);

                        QuiltNode newNode = new QuiltNode(nextNodeId++, edgeType.Item2);
                        if (drawGraph)
                        {
                            this.graphDrawer.JoinNode(newNode, WrapRange(activeBorder, startIndex, index));
                        }

                        int nextIndex = WrapIndex(startIndex + 1, activeBorder.Count);
                        foreach (QuiltNode node in WrapRange(activeBorder, startIndex + 1, index - 1))
                        {
                            activeBorder.Remove(node);
                        }

                        activeBorder.Insert(WrapIndex(edges.LastOrDefault().Index, activeBorder.Count), newNode);
                        index = nextIndex;

                        edges.RemoveAt(edges.Count - 1);
                    }
                    else
                    {
                        edges.Add(new QuiltEdge(edgeType, index));
                    }
                }

                index++;
            }

            QuiltNode capstoneNode = new QuiltNode(nextNodeId++, Colour.Blue);
            if (drawGraph)
            {
                this.graphDrawer.JoinNode(capstoneNode, activeBorder);
            }

            this.NumTricolourTriangles = edges.Count / 3;
        }
    }
}
