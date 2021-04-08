using System;
using Battleships.Logic;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class ShipGeneratorTests
    {
        [Fact]
        public void Constructor_InvalidValue_ThrowsArgumentOutOfException()
        {
            Action action = () => new ShipGenerator(0);

            action.Should().Throw<ArgumentOutOfRangeException>();
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
            shipGenerator.Invoking(sg => sg.CreateShip(0)).Should().Throw<ArgumentOutOfRangeException>();
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

            shipGenerator.Invoking(s => s.CreateShip(3)).Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}