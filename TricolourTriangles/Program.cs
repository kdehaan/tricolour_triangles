using System;
using System;
using System.Collections.Generic;

namespace TricolourTriangles
{

    /// <summary>
    /// 
    /// </summary>
    class ColourTriangle
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input a perimeter length (must be >= 3):");
            //while(true)
            //{
            string input = Console.ReadLine();
            try
            {
                PolygonGraph polygon = new PolygonGraph(int.Parse(input));
            }
            catch (Exception e) when (e is ArgumentException || e is FormatException)
            {
                Console.WriteLine(e.Message);
            }
            //}

        }
    }
}