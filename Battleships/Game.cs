using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    public class Game
    {
        private const int BattleshipCount = 1;

        private const int DestroyerCount = 2;

        private readonly List<string> _usedLocations;

        public Game()
        {
            Ships = new List<Ship>();
            _usedLocations = new List<string>();
            CreateBattleships();
            CreateDestroyers();
        }

        public enum Direction
        {
            Vertical,
            Horizontal
        }

        public List<Ship> Ships { get; }

        public bool Shoot(string location)
        {
            location = string.Concat(location.Where(c => !char.IsWhiteSpace(c)))
                .ToUpper();

            if (!IsValidLocation(location))
                throw new ArgumentOutOfRangeException(location);

            if (_usedLocations.Contains(location))
                throw new ArgumentException($"Location '{location}' was previously used.");

            _usedLocations.Add(location);

            var ship = Ships.SingleOrDefault(s => s.Locations.Contains(location));

            if (ship == null)
                return false;

            ship.DamagedLocations.Add(location);
            return true;
        }

        public string GetRandomLocation(int xOffset = 0, int yOffset = 0)
        {
            var random = new Random();
            var randomChar = (char) random.Next(65, 75 - xOffset);
            var randomNumber = random.Next(1, 11 - yOffset);
            return $"{randomChar}{randomNumber}";
        }

        private void CreateBattleships()
            => CreateShips(5, BattleshipCount);

        private void CreateDestroyers()
            => CreateShips(4, DestroyerCount);

        private void CreateShips(int size, int count)
        {
            for (var i = count; i > 0; i--)
            {
                List<string> locations;

                do
                {
                    locations = GetRandomShipLocation(size);
                } while (Ships.Any(s => locations.Any(location => s.Locations.Contains(location))));

                var ship = new Ship {DamagedLocations = new List<string>(), Locations = locations};
                Ships.Add(ship);
            }
        }

        private List<string> GetRandomShipLocation(int size)
        {
            var random = new Random();
            var direction = (Direction) random.Next(0, 2);
            var randomLocation = new List<string>();
            var startPosition = direction == Direction.Vertical ? GetRandomLocation(size) : GetRandomLocation(0, size);
            var number = int.Parse(startPosition[1..]);

            for (var i = 0; i < size; i++)
                randomLocation.Add(
                    direction == Direction.Vertical ?
                        $"{(char) (startPosition[0] + i)}{number}" :
                        $"{startPosition[0]}{number + i}"
                );

            return randomLocation;
        }

        private bool IsValidLocation(string location)
        {
            // Check length
            if (location.Length < 2 || location.Length > 3)
                return false;

            // Check char part
            if (location[0] < 65 || location[0] > 74)
                return false;

            // Check number part
            return int.TryParse(location[1..], out var numIndex) && numIndex >= 1 && numIndex <= 10;
        }
    }
}