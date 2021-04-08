using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Logic
{
    public class ShipGenerator
    {
        private readonly int _playgroundSize;
        private readonly List<Location> _usedLocations;

        public ShipGenerator(int playgroundSize)
        {
            if (playgroundSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(playgroundSize));
            }

            _playgroundSize = playgroundSize;
            _usedLocations = new List<Location>();
            Ships = new List<IShip>();
        }

        public List<IShip> Ships { get; }

        public void CreateShip(int shipSize)
        {
            if (shipSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(shipSize));
            }

            var possibleLocations = GetAllPossibleCombinations(shipSize);

            if (possibleLocations.Count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(shipSize));
            }

            var random = new Random();
            var randomIndex = random.Next(0, possibleLocations.Count);
            var randomLocations = possibleLocations[randomIndex];

            AddShipToPropertyShips(randomLocations);

            _usedLocations.AddRange(randomLocations);
        }

        private void AddShipToPropertyShips(IEnumerable<Location> locations)
        {
            var locationDictionary = locations.ToDictionary(l => l, l => false);

            var ship = new Ship
            {
                Locations = locationDictionary
            };

            Ships.Add(ship);
        }

        private List<List<Location>> GetAllPossibleCombinations(int shipSize)
        {
            var listOfAllPossibleLocations = new List<List<Location>>();

            var maxLocationIndex = _playgroundSize - shipSize;

            // For iteration over alpha
            for (var i = 0; i < maxLocationIndex; i++)
            {
                // For iteration over number
                for (var j = 1; j <= maxLocationIndex; j++)
                {
                    var verticalLocations = new List<Location>();
                    var horizontalLocations = new List<Location>();

                    for (var k = 0; k < shipSize; k++)
                    {
                        var verticalLocation = new Location
                        {
                            Alpha = (char)(65 + i),
                            Number = j + k
                        };

                        var horizontalLocation = new Location
                        {
                            Alpha = (char)(65 + i + k),
                            Number = j
                        };

                        verticalLocations.Add(verticalLocation);
                        horizontalLocations.Add(horizontalLocation);
                    }

                    listOfAllPossibleLocations.Add(verticalLocations);
                    listOfAllPossibleLocations.Add(horizontalLocations);
                }
            }

            RemoveLocationsUsedBefore(ref listOfAllPossibleLocations);
            return listOfAllPossibleLocations;
        }

        private void RemoveLocationsUsedBefore(ref List<List<Location>> locations)
        {
            foreach (var list in from list in locations.ToArray()
                                 from usedLocation in _usedLocations.Where(list.Contains)
                                 select list)
            {
                locations.Remove(list);
            }
        }
    }
}