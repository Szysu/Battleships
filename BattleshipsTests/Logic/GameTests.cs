using System;
using System.Collections.Generic;
using Battleships.Logic;
using Moq;
using Xunit;

namespace BattleshipsTests.Logic
{
    public class GameTests
    {
        [Fact]
        public void Shoot_InvalidLocation_ThrowsArgumentOutOfRangeException()
        {
            var playgroundMock = new Mock<IPlayground>();
            playgroundMock.Setup(s => s.IsValidLocation(It.IsAny<Location>())).Returns(false);

            var game = new Game(playgroundMock.Object, new List<Ship>());
            Assert.Throws<ArgumentOutOfRangeException>(() => { game.Shoot(new Location('Z', 100)); });
        }

        [Fact]
        public void Shoot_DuplicateShots_ThrowsArgumentException()
        {
            var playgroundMock = new Mock<IPlayground>();
            playgroundMock.Setup(s => s.IsValidLocation(It.IsAny<Location>())).Returns(true);

            var game = new Game(playgroundMock.Object, new List<Ship>());
            game.Shoot(new Location('A', 1));

            Assert.Throws<ArgumentException>(() => { game.Shoot(new Location('A', 1)); });
        }

        [Fact]
        public void Shoot_ValidLocation_MarksLocationAsShot()
        {
            var playgroundMock = new Mock<IPlayground>();
            playgroundMock.Setup(s => s.IsValidLocation(It.IsAny<Location>())).Returns(true);

            var location = new Location('A', 1);

            var ship = new Ship
            {
                Locations = new Dictionary<Location, bool> {{location, false}}
            };

            var game = new Game(playgroundMock.Object, new List<Ship> {ship});
            game.Shoot(location);

            Assert.Contains(location, game.Shots);
        }
    }
}