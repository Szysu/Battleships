using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Logic
{
    public class Game : IGame
    {
        public Game(IPlayground playground, IList<Ship> ships)
        {
            Playground = playground;
            Ships = ships;
            Shots = new List<Location>();
        }

        public IList<Ship> Ships { get; }
        public List<Location> Shots { get; }
        public IPlayground Playground { get; }
        public bool IsEnded => Ships.All(s => s.IsSunk);

        public bool Shoot(Location location)
        {
            if (!Playground.IsValidLocation(location))
            {
                throw new ArgumentOutOfRangeException(nameof(location));
            }

            if (Shots.Contains(location))
            {
                throw new ArgumentException("Location was previously shot.", nameof(location));
            }

            Shots.Add(location);

            var ship = Ships.SingleOrDefault(s => s.Locations.ContainsKey(location));
            if (ship == null)
            {
                return false;
            }

            ship.Locations[location] = true;
            return true;
        }
    }
}