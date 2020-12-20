﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TricolourTriangles
{
    public class PolygonGraph
    {
        private Random random = new Random();
        private List<Colour> Border;

        public PolygonGraph(List<Colour> border)
        {
            Border = border;
        }

        public PolygonGraph(int perimeterLength)
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

        private IEnumerable<int> GetNodeName()
        {
            int index = 0;
            yield return index++;
        }
    }


}