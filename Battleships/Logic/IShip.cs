using System.Collections.Generic;

namespace Battleships.Logic
{
    public interface IShip
    {
        bool IsSunk { get; }
        IDictionary<Location, bool> Locations { get; init; }
    }
}