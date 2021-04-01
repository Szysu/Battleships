using System.Collections.Generic;

namespace Battleships
{
    public class Ship
    {
        public bool IsSunk => (DamagedLocations.Count == Locations.Count);

        public List<string> Locations { get; set; }
        public List<string> DamagedLocations { get; set; }
    }
}