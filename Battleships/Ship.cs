using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships
{
    /// <summary>
    ///     A class for storing the ship locations and their condition.
    /// </summary>
    public class Ship
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Ship"/> class.
        /// </summary>
        /// <param name="locations">The ship locations.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="locations"/> is null or empty.
        /// </exception>
        public Ship(ICollection<Location> locations)
        {
            if (locations == null || !locations.Any())
            {
                throw new ArgumentNullException(nameof(locations));
            }

            Locations = new Dictionary<Location, bool>();

            foreach (var location in locations)
            {
                Locations.Add(location, false);
            }
        }

        /// <summary>
        ///     Indicates whether all locations have been hit.
        /// </summary>
        public bool IsSunk => Locations.All(loc => loc.Value);

        /// <summary>
        ///     A dictionary containing the ship locations and their condition.
        /// </summary>
        public Dictionary<Location, bool> Locations { get; }
    }
}