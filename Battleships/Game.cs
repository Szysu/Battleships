using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships
{
    /// <summary>
    ///     The main class of the game.
    /// </summary>
    public class Game
    {
        private const int BattleshipSize = 5;
        private const int DestroyerSize = 4;

        private readonly List<Location> _missedShotLocations;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game(int battleshipsToCreate, int destroyersToCreate)
        {
            Ships = new List<Ship>();
            _missedShotLocations = new List<Location>();
            CreateShips(BattleshipSize, destroyersToCreate);
            CreateShips(DestroyerSize, battleshipsToCreate);
        }

        /// <summary>
        ///     A list of created ships.
        /// </summary>
        public List<Ship> Ships { get; }

        /// <summary>
        ///     The number of battleships to sink.
        /// </summary>
        public int BattleshipsToSink => Ships.Count(s => s.Locations.Count == 5 && !s.IsSunk);

        /// <summary>
        ///     The number of destroyers to sink.
        /// </summary>
        public int DestroyersToSink => Ships.Count(s => s.Locations.Count == 4 && !s.IsSunk);

        /// <summary>
        ///     Indicates whether all ships have been sunk.
        /// </summary>
        public bool IsEnded => Ships.All(s => s.IsSunk);

        /// <summary>
        ///     Compares ships locations with the specified <paramref name="location"/> .
        /// </summary>
        /// <param name="location">A location to compare.</param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="location"/> was previously shot.
        /// </exception>
        /// <returns>
        ///     <see langword="true"/> if the specified <paramref name="location"/> is a ship
        ///     location, otherwise <see langword="false"/> .
        /// </returns>
        public bool Shoot(Location location)
        {
            if (_missedShotLocations.Contains(location))
                throw new ArgumentException("Location was previously shot.", nameof(location));

            if (TryGetShipLocationCondition(location, out var ship, out var condition))
            {
                if (condition) throw new ArgumentException("Location was previously shot.", nameof(location));

                ship.Locations[location] = true;
                return true;
            }

            _missedShotLocations.Add(location);
            return false;
        }

        /// <summary>
        ///     Returns a symbol representation of the specified <paramref name="location"/> .
        /// </summary>
        /// <param name="location">The location of the symbol.</param>
        /// <returns>The symbol of the location.</returns>
        public char GetLocationSymbol(Location location)
        {
            var symbol = '#';

            if (_missedShotLocations.Contains(location)) symbol = 'O';

            if (TryGetShipLocationCondition(location, out _, out var condition) && condition) symbol = 'X';

            return symbol;
        }

        /// <summary>
        ///     Returns a string representing the game's playground.
        /// </summary>
        /// <returns>A string representing the game's playground.</returns>
        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLine("  1 2 3 4 5 6 7 8 9 10");

            for (var i = 'A'; i <= 'J'; i++)
            {
                str.Append(i);

                for (var j = 1; j <= 10; j++)
                {
                    var currentLocation = new Location(i, j);
                    str.Append($" {GetLocationSymbol(currentLocation)}");
                }

                str.AppendLine();
            }

            return str.ToString();
        }

        private void CreateShips(int size, int count)
        {
            for (var i = count; i > 0; i--)
            {
                List<Location> locations;

                do
                {
                    locations = GetRandomShipLocation(size);
                } while (locations.Any(l => TryGetShipLocationCondition(l, out _, out _)));

                var ship = new Ship(locations);
                Ships.Add(ship);
            }
        }

        private bool TryGetShipLocationCondition(Location location, out Ship ship, out bool condition)
        {
            ship = default;
            condition = default;

            foreach (var s in Ships)
            {
                if (!s.Locations.TryGetValue(location, out var value)) continue;

                ship = s;
                condition = value;
            }

            return ship != null;
        }

        private List<Location> GetRandomShipLocation(int size)
        {
            var locations = new List<Location>();
            var isVertical = new Random().Next(0, 2) == 0;
            var firstLocation = isVertical ? Location.Random(size, 0) : Location.Random(0, size);

            for (var i = 0; i < size; i++)
                locations.Add(
                    isVertical
                        ? new Location((char)(firstLocation.Char + i), firstLocation.Number)
                        : new Location(firstLocation.Char, firstLocation.Number + i)
                );

            return locations;
        }
    }
}