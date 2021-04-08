using System.Collections.Generic;

namespace Battleships.Logic
{
    public interface IGame
    {
        IList<IShip> Ships { get; }
        List<Location> Shots { get; }
        IPlayground Playground { get; }
        bool IsEnded { get; }

        bool Shoot(Location location);
    }
}