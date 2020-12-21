using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MarsExpedition.Tests
{
    public class MapTests
    {

        [Test]
        public void CreateMapTest()
        {
            int height = 10, width = 10;
            var createdMap = MapFactory.CreateMap(height, width);

            Assert.AreEqual(createdMap.MaxGridPoint.X, width);
            Assert.AreEqual(createdMap.MaxGridPoint.Y, height);
        }
    }
}
