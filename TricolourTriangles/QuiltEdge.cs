// <copyright file="QuiltEdge.cs" company="Kevin de Haan (github.com/kdehaan)">
// Written by Kevin de Haan (github.com/kdehaan)
// </copyright>

namespace TricolourTriangles
{
    /// <summary>
    /// Represents the id (unique name) and type (colour) of a node in a quilt.
    /// </summary>
    public struct QuiltEdge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuiltEdge"/> struct.
        /// </summary>
        /// <param name="type">Colour-to-colour order transition (ex., Red-Green or Green-Blue).</param>
        /// <param name="index">Array position of the second node in the active perimeter.</param>
        public QuiltEdge((Colour, Colour) type, int index)
        {
            this.Type = type;
            this.Index = index;
        }

        /// <summary>
        /// Gets the colour-to-colour order transition (ex., Red-Green or Green-Blue)
        /// </summary>
        public (Colour, Colour) Type { get; }

        /// <summary>
        /// Gets the array position of the second node in the active perimeter.
        /// </summary>
        public int Index { get; }
    }
}
