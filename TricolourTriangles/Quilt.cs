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
        private int currentId = 0;
        private GraphDrawer graphDrawer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Quilt"/> class using a specific border.
        /// </summary>
        /// <param name="border">A list of polygon nodes to use as a border.</param>
        public Quilt(List<QuiltNode> border)
        {
            this.Border = border;
            this.currentId = border.Count;
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quilt"/> class with random border nodes.
        /// </summary>
        /// <param name="perimeterLength">The length of the random perimeter.</param>
        public Quilt(int perimeterLength)
        {
            this.Border = CreateBorder(perimeterLength);
            this.currentId = perimeterLength;
            this.Initialize();
        }

        /// <summary>
        /// Gets the list of nodes representing the outermost perimeter of the quilt.
        /// </summary>
        public List<QuiltNode> Border { get; private set; }

        /// <summary>
        /// Gets the minimum number of three colour triangles possible for this quilt.
        /// </summary>
        public int NumTriangles { get; private set; }

        private void Initialize()
        {
            this.graphDrawer = new GraphDrawer(this.Border);
            this.FindMinTricolourTriangles();
        }

        private void FindMinTricolourTriangles()
        {
            List<QuiltNode> activeBorder = new List<QuiltNode>(this.Border);

        }

        /// <summary>
        /// Draws the quilt as a graph.
        /// </summary>
        public void Visualise()
        {
            this.graphDrawer.DrawGraph();
        }

        private static List<QuiltNode> CreateBorder(int perimeterLength)
        {
            Random random = new Random();
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


        private int GetNextId()
        {
            return this.currentId++;
        }
    }
}
