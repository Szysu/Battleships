using System.Collections.Generic;
using Battleships.Logic;
using Xunit;

namespace BattleshipsTests.Logic
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
            Assert.False(ship.IsSunk);
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

            Assert.True(ship.IsSunk);
        }
    }
}