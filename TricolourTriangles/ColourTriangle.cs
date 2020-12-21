// <copyright file="ColourTriangle.cs" company="Kevin de Haan (github.com/kdehaan)">
// Written by Kevin de Haan (github.com/kdehaan)
// </copyright>

namespace TricolourTriangles
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Finds a way to fill a 'quilt' with triangles such that the fewest amount of 'tricolour'
    /// triangles are produced.
    /// </summary>
    public class ColourTriangle
    {
        /// <summary>
        /// Main console function.
        /// </summary>
        /// <param name="args">command line arguments.</param>
        public static void Main()
        {
            Console.WriteLine("Input a perimeter length (must be >= 3):");

            string input = Console.ReadLine();
            try
            {
                Quilt quilt = new Quilt(int.Parse(input));
                GraphDrawer drawer = new GraphDrawer(quilt.Border);
                drawer.DrawGraph();
            }
            catch (Exception e) when (e is ArgumentException || e is FormatException)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}