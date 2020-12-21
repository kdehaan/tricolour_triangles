using System;
using Xunit;
using System.Collections.Generic;

namespace TricolourTriangles.UnitTests
{
    
    public class QuiltTest
    {

        static readonly List<QuiltNode> sampleBorder = new List<QuiltNode>(
            new QuiltNode[] {
                new QuiltNode(0, Colour.Red),
                new QuiltNode(1, Colour.Green),
                new QuiltNode(2, Colour.Blue),
            });
        static readonly List<QuiltNode> sampleBorderMin3 = new List<QuiltNode>(
            new QuiltNode[] {
                new QuiltNode(0, Colour.Red),
                new QuiltNode(1, Colour.Green),
                new QuiltNode(2, Colour.Blue),
                new QuiltNode(4, Colour.Red),
                new QuiltNode(5, Colour.Green),
                new QuiltNode(6, Colour.Blue),
                new QuiltNode(7, Colour.Red),
                new QuiltNode(8, Colour.Green),
                new QuiltNode(9, Colour.Blue),
            });
        //private readonly Polygon samplePolygon = new Polygon(sampleBorder);

        [Theory]
        [InlineData(3)]
        [InlineData(65536)] //2^16
        public void TestPerimeterInitPass(int perimeterLength)
        {
            _ = new MinColourTriangleQuilt(perimeterLength, false);
        }


        [Theory]
        [InlineData(-4)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(2)]
        public void TestPerimeterInitFail(int perimeterLength)
        {
            void initQuilt() => new MinColourTriangleQuilt(perimeterLength, false);
            Assert.Throws<ArgumentException>(() => initQuilt());
        }

        [Fact]
        public void TestGetBorder()
        {
            MinColourTriangleQuilt quilt = new MinColourTriangleQuilt(sampleBorder, false);
            Assert.Equal(sampleBorder, quilt.Border);
        }

        [Fact]
        public void TestMin3()
        {
            MinColourTriangleQuilt quilt = new MinColourTriangleQuilt(sampleBorderMin3, false);
            Assert.Equal(3, quilt.NumTricolourTriangles);
        }

        [Fact(Timeout = 4000)]
        public void TestLargeGraph()
        {
            MinColourTriangleQuilt quilt = new MinColourTriangleQuilt(60000, false);
            Assert.True(quilt.NumTricolourTriangles > -1);
        }
    }
}

