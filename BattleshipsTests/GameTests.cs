using System;
using System.Collections.Generic;
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
            _game = new Game(Program.NumberOfBattleshipsToCreate, Program.NumberOfDestroyersToCreate);
        }

        [Fact]
        public void PropertyShips_ReturnsList()
        {
            Assert.IsType<List<Ship>>(_game.Ships);
        }

        [Fact]
        public void PropertyBattleshipsToSink_ReturnsCorrectNumber()
        {
            var battleshipsToSink = _game.Ships.Count(s => s.Locations.Count == 5 && !s.IsSunk);
            Assert.Equal(battleshipsToSink, _game.BattleshipsToSink);
        }

        [Fact]
        public void PropertyDestroyersToSink_ReturnsCorrectNumber()
        {
            var destroyersToSink = _game.Ships.Count(s => s.Locations.Count == 4 && !s.IsSunk);
            Assert.Equal(destroyersToSink, _game.DestroyersToSink);
        }

        [Fact]
        public void PropertyIsEnded_ReturnsCorrectBoolean()
        {
            Assert.False(_game.IsEnded);

            // Changes the condition of all ships' locations to sink.
            _game.Ships.ForEach(s => s.Locations.Keys.ToList().ForEach(k => s.Locations[k] = true));

            Assert.True(_game.IsEnded);
        }

        [Fact]
        public void Shoot_NormalShot_ReturnsCorrectBoolean()
        {
            // Missed shot
            var location = GetEmptyLocation();
            Assert.False(_game.Shoot(location));

            // Hit shot
            location = GetShipLocation();
            Assert.True(_game.Shoot(location));
        }

        [Fact]
        public void Shoot_DuplicateShots_ThrowsArgumentException()
        {
            // Missed shots
            var location = GetEmptyLocation();
            _game.Shoot(location);
            Assert.Throws<ArgumentException>(() => _game.Shoot(location));

            // Hit shots
            location = GetShipLocation();
            _game.Shoot(location);
            Assert.Throws<ArgumentException>(() => _game.Shoot(location));
        }

        [Fact]
        public void GetLocationSymbol_ReturnsCorrectSymbols()
        {
            // Not shot
            Assert.Equal('#', _game.GetLocationSymbol(GetEmptyLocation()));

            // Missed shot
            var location = GetEmptyLocation();
            _game.Shoot(location);
            Assert.Equal('O', _game.GetLocationSymbol(location));

            // Hit shot
            location = GetShipLocation();
            _game.Shoot(location);
            Assert.Equal('X', _game.GetLocationSymbol(location));
        }

        [Fact]
        public void ToString_ReturnsCorrectString()
        {
            var expectedString = string.Format(
                "  1 2 3 4 5 6 7 8 9 10{0}"
                + "A # # # # # # # # # #{0}"
                + "B # # # # # # # # # #{0}"
                + "C # # # # # # # # # #{0}"
                + "D # # # # # # # # # #{0}"
                + "E # # # # # # # # # #{0}"
                + "F # # # # # # # # # #{0}"
                + "G # # # # # # # # # #{0}"
                + "H # # # # # # # # # #{0}"
                + "I # # # # # # # # # #{0}"
                + "J # # # # # # # # # #{0}",
                Environment.NewLine);

            var gameString = _game.ToString();
            Assert.Equal(expectedString, gameString);
        }

        private Location GetEmptyLocation()
        {
            Location emptyLocation;

            // Checks if any location in any ship is the same as a randomly generated location.
            do
            {
                emptyLocation = Location.Random(0, 0);
            } while (_game.Ships.Any(ship => ship.Locations.ContainsKey(emptyLocation)));

            return emptyLocation;
        }

        private Location GetShipLocation()
        {
            var ship = _game.Ships[0];
            return ship.Locations.Keys.First();
        }
    }
}