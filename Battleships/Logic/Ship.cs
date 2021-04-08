using System.Collections.Generic;
using System.Linq;

namespace Battleships.Logic
{
    public class Ship : IShip
    {
        public bool IsSunk => Locations.All(loc => loc.Value);

        public IDictionary<Location, bool> Locations { get; init; }
    }
}