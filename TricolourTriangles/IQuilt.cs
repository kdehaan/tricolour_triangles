// <copyright file="IQuilt.cs" company="Kevin de Haan (github.com/kdehaan)">
// Written by Kevin de Haan (github.com/kdehaan)
// </copyright>

namespace TricolourTriangles
{
    using System.Collections.Generic;

    /// <summary>
    /// Describes an n-bordered graph with nodes outlining interior 'patches'.
    /// </summary>
    public interface IQuilt
    {
        /// <summary>
        /// Gets the list of nodes representing the outermost perimeter of the quilt.
        /// </summary>
        List<QuiltNode> Border { get; }

        /// <summary>
        /// Draws the quilt as a graph.
        /// </summary>
        void Visualise();
    }
}
