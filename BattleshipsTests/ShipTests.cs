using System.Collections.Generic;
using Battleships.Logic;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class ShipTests
    {
        [Fact]
        public void PropertyIsSunk_CorrectlyIndicates()
        {
            var locations = new Dictionary<Location, bool>();
            locations.Add(new Location('A', 1), false);

            var ship = new Ship
            {
                Locations = locations
            };

            ship.IsSunk.Should().BeFalse();
        }

        [Fact]
        public void PropertyIsSunk_AllShipsSunk_ReturnsTrue()
        {
            var locations = new Dictionary<Location, bool>();
            locations.Add(new Location('A', 1), true);

            var ship = new Ship
            {
                Locations = locations
            };

            ship.IsSunk.Should().BeTrue();
        }
    }
}