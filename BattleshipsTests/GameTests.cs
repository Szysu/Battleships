using System;
using System.Linq;
using Battleships;
using Xunit;

namespace BattleshipsTests
{
    public class GameTests
    {
        private readonly Game _game;

        public GameTests()
        {
            _game = new Game();
        }

        [Fact]
        public void Constructor_NewInstance()
        {
            var game = new Game();
            Assert.NotNull(game);
        }

        [Fact]
        public void Shoot_ShipLocation_ReturnsTrue()
        {
            var ship = _game.Ships[0];
            var shipLocation = ship.Locations[0];
            var isHit = _game.Shoot(shipLocation);
            Assert.True(isHit);
        }

        [Fact]
        public void Shoot_EmptyLocation_ReturnsFalse()
        {
            string randomLocation;

            do
            {
                randomLocation = _game.GetRandomLocation();
            } while (_game.Ships.Any(ship => ship.Locations.Contains(randomLocation)));

            var isHit = _game.Shoot(randomLocation);
            Assert.False(isHit);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ​")]
        [InlineData("A")]
        [InlineData("1")]
        [InlineData("A0")]
        [InlineData("K1")]
        [InlineData("A11")]
        [InlineData("K11")]
        [InlineData("6549")] // 65 = 'A' & 49 = '1' in ASCII
        [InlineData("652")]
        [InlineData("A49")]
        [InlineData("A-1")]
        [InlineData("A-11")]
        [InlineData("K-1")]
        [InlineData("K-11")]
        public void Shoot_InvalidLocation_ThrowsArgumentOutOfRangeException(string location)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _game.Shoot(location));
        }

        [Fact]
        public void Shoot_DuplicateShoots_ThrowsArgumentException()
        {
            var randomLocation = _game.GetRandomLocation();

            _game.Shoot(randomLocation);

            Assert.Throws<ArgumentException>(() => _game.Shoot(randomLocation));
        }
    }
}