// <copyright file="PolygonNode.cs" company="Kevin de Haan (github.com/kdehaan)">
// Written by Kevin de Haan (github.com/kdehaan)
// </copyright>

namespace TricolourTriangles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the id (unique name) and type (colour) of a node in a polygon.
    /// </summary>
    public struct PolygonNode
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonNode"/> struct.
        /// </summary>
        /// <param name="id">Unique node identifier (uniqueness not implicitly guaranteed).</param>
        /// <param name="type">One of Red, Blue or Green (enum type).</param>
        public PolygonNode(int id, Colour type)
        {
            this.Id = id;
            this.Type = type;
        }

        /// <summary>
        /// Gets the unique id of the node. Positive integer.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the type of the node. Can be Red, Blue or Green.
        /// </summary>
        public Colour Type { get; private set; }
    }
}
