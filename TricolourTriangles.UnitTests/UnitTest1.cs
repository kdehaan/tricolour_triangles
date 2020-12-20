using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xunit;

namespace TricolourTriangles.UnitTests
{
    public class PolygonTest
    {

        [Theory]
        [InlineData(3)]
        [InlineData(Int32.MaxValue)]
        public void PerimeterInitPass(int perimeterLength)
        {
            Polygon polygon = new Polygon(perimeterLength);
        }


        [Theory]
        [InlineData(-4)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(2)]
        public void PerimeterInitFail(int perimeterLength)
        {
            Action initPolygon = () => new Polygon(perimeterLength);
            Assert.Throws<ArgumentException>(() => initPolygon.Invoke());
        }


    }
}
