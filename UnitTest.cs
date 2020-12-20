using System;
using Xunit;


namespace TricolourTriangles.UnitTests
{
    public class PolygonTest
    {

        [Theory]
        [InlineData(3)]
        [InlineData(65536)] //2^16
        public void PerimeterInitPass(int perimeterLength)
        {
            PolygonGraph polygon = new PolygonGraph(perimeterLength);
        }


        [Theory]
        [InlineData(-4)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(2)]
        public void PerimeterInitFail(int perimeterLength)
        {
            Action initPolygon = () => new PolygonGraph(perimeterLength);
            Assert.Throws<ArgumentException>(() => initPolygon.Invoke());
        }

    }
}
