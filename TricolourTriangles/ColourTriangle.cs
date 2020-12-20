

namespace TricolourTriangles
{

    using System;
    using System.Collections.Generic;

    public class ColourTriangle
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Input a perimeter length (must be >= 3):");
  
            string input = Console.ReadLine();
            try
            {
                Polygon polygon = new Polygon(int.Parse(input));
                GraphDrawer drawer = new GraphDrawer(polygon);
                drawer.DrawGraph();
            }
            catch (Exception e) when (e is ArgumentException || e is FormatException)
            {
                Console.WriteLine(e.Message);
            }



        }
    }
}