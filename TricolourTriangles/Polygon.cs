// <copyright file="Polygon.cs" company="Kevin de Haan (github.com/kdehaan)">
// Written by Kevin de Haan (github.com/kdehaan)
// </copyright>

namespace TricolourTriangles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a polygon's innermost and outermost perimeters.
    /// </summary>
    public class Polygon
    {

        private readonly Random random = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class using a specific border.
        /// </summary>
        /// <param name="border">A list of polygon nodes to use as a border.</param>
        public Polygon(List<PolygonNode> border)
        {
            this.Border = border;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class with random nodes.
        /// </summary>
        /// <param name="perimeterLength">The lenth of the random perimiter.</param>
        public Polygon(int perimeterLength)
        {
            if (perimeterLength < 3)
            {
                throw new ArgumentException("Parameter cannot be less than 3", nameof(perimeterLength));
            }

            Array colourTypes = Enum.GetValues(typeof(Colour));
            this.Border = Enumerable
                .Range(0, perimeterLength)
                .Select(i => new PolygonNode(1, (Colour)colourTypes.GetValue(this.random.Next(colourTypes.Length))))
                .ToList();
        }

        /// <summary>
        /// Gets the list of nodes representing the outermost perimeter of the polygon.
        /// </summary>
        public List<PolygonNode> Border { get; private set; }


    }


}
