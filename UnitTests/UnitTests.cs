using System;
using Xunit;
using System.Collections.Generic;



namespace TricolourTriangles.UnitTests
{
    
    public class PolygonTest
    {

        static readonly List<Colour> sampleBorder = new List<Colour>(new Colour[] { Colour.Red, Colour.Blue, Colour.Green });
        private readonly Polygon samplePolygon = new Polygon(sampleBorder);

        [Theory]
        [InlineData(3)]
        [InlineData(65536)] //2^16
        public void TestPerimeterInitPass(int perimeterLength)
        {
            Polygon polygon = new Polygon(perimeterLength);
        }


        [Theory]
        [InlineData(-4)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(2)]
        public void TestPerimeterInitFail(int perimeterLength)
        {
            Action initPolygon = () => new Polygon(perimeterLength);
            Assert.Throws<ArgumentException>(() => initPolygon.Invoke());
        }

        [Fact]
        public void TestGetBorder()
        {

            Polygon polygon = new Polygon(sampleBorder);
            Assert.Equal(sampleBorder, polygon.GetBorder());

        }

    }
}

