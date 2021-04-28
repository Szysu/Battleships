using System.Collections.Generic;

namespace Battleships.Logic
{
    public interface IShipGenerator
    {
        public List<IShip> Ships { get; }
        public void CreateShip(int shipSize);
    }
}