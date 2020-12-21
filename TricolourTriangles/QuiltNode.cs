// <copyright file="QuiltNode.cs" company="Kevin de Haan (github.com/kdehaan)">
// Written by Kevin de Haan (github.com/kdehaan)
// </copyright>

namespace TricolourTriangles
{
    /// <summary>
    /// Represents the id (unique name) and type (colour) of a node in a quilt.
    /// </summary>
    public struct QuiltNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuiltNode"/> struct.
        /// </summary>
        /// <param name="id">Unique node identifier (uniqueness not implicitly guaranteed).</param>
        /// <param name="type">One of Red, Blue or Green (enum type).</param>
        public QuiltNode(int id, Colour type)
        {
            this.Id = id;
            this.Type = type;
        }

        /// <summary>
        /// Gets the unique id of the node. Positive integer.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the type of the node. Can be Red, Blue or Green.
        /// </summary>
        public Colour Type { get; }
    }
}
