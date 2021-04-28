using System;
using Battleships.Logic;
using Xunit;

namespace BattleshipsTests.Logic
{
    public class ShipGeneratorTests
    {
        [Fact]
        public void Constructor_InvalidValue_ThrowsArgumentOutOfException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { new ShipGenerator(0); });
        }

        [Fact]
        public void Constructor_ValidValue_ReturnsVoid()
        {
            new ShipGenerator(10);
        }

        [Fact]
        public void CreateShip_InvalidValue_ThrowsArgumentOutOfRangeException()
        {
            var shipGenerator = new ShipGenerator(10);
            Assert.Throws<ArgumentOutOfRangeException>(() => { shipGenerator.CreateShip(0); });
        }

        [Fact]
        public void CreateShip_ValidValue_ReturnsVoid()
        {
            var shipGenerator = new ShipGenerator(10);
            shipGenerator.CreateShip(3);
        }

        [Fact]
        public void CreateShip_NoFreeCombinations_ThrowsArgumentOutOfRangeException()
        {
            var shipGenerator = new ShipGenerator(2);

            Assert.Throws<ArgumentOutOfRangeException>(() => { shipGenerator.CreateShip(3); });
        }
    }
}