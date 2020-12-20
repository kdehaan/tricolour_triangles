using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TricolourTriangles
{
    
    public class Polygon
    {
        private Random random = new Random();
        private List<Colour> Border;

        public Polygon(List<Colour> border)
        {
            Border = border;
        }

        public Polygon(int perimeterLength)
        {
            if (perimeterLength < 3)
            {
                throw new ArgumentException("Parameter cannot be less than 3", nameof(perimeterLength));
            }

            Array colourTypes = Enum.GetValues(typeof(Colour));
            Border = Enumerable
                .Range(0, perimeterLength)
                .Select(i => (Colour)colourTypes.GetValue(random.Next(colourTypes.Length)))
                .ToList();

        }

        public List<Colour> GetBorder()
        {
            return Border;
        }


        
    }


}
