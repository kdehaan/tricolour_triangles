using System;
using Xunit;
using System.Collections.Generic;



namespace TricolourTriangles.UnitTests
{
    
    public class PolygonTest
    {

        static readonly List<PolygonNode> sampleBorder = new List<PolygonNode>(
            new PolygonNode[] {
                new PolygonNode(0, Colour.Red),
                new PolygonNode(1, Colour.Green),
                new PolygonNode(2, Colour.Blue),
            });
        //private readonly Polygon samplePolygon = new Polygon(sampleBorder);

        [Theory]
        [InlineData(3)]
        [InlineData(65536)] //2^16
        public void TestPerimeterInitPass(int perimeterLength)
        {
            _ = new Polygon(perimeterLength);
        }


        [Theory]
        [InlineData(-4)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(2)]
        public void TestPerimeterInitFail(int perimeterLength)
        {
            void initPolygon() => new Polygon(perimeterLength);
            Assert.Throws<ArgumentException>(() => initPolygon());
        }

        [Fact]
        public void TestGetBorder()
        {
            Polygon polygon = new Polygon(sampleBorder);
            Assert.Equal(sampleBorder, polygon.Border);
        }

    }
}

