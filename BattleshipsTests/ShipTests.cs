using System;
using System.Collections.Generic;
using System.Linq;
using Battleships;
using Xunit;

namespace BattleshipsTests
{
    public class ShipTests
    {
        private readonly Ship _ship;

        public ShipTests()
        {
            var list = new List<Location>
            {
                new Location('A', 1),
                new Location('A', 2),
                new Location('A', 3),
                new Location('A', 4)
            };

            _ship = new Ship(list);
        }

        [Fact]
        public void PropertyLocations_ReturnsDictionary()
        {
            Assert.IsType<Dictionary<Location, bool>>(_ship.Locations);
        }

        [Fact]
        public void PropertyIsSunk_ReturnsCorrectBoolean()
        {
            Assert.False(_ship.IsSunk);

            // Sets all ship locations as hit
            _ship.Locations.Keys.ToList().ForEach(k => _ship.Locations[k] = true);

            Assert.True(_ship.IsSunk);
        }

        [Fact]
        public void Constructor_InvalidIEnumerable_ThrowsArgumentNullException()
        {
            var list = new List<Location>();
            Assert.Throws<ArgumentNullException>(() => new Ship(list));
            Assert.Throws<ArgumentNullException>(() => new Ship(null));
        }
    }
}