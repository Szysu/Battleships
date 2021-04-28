using System.Collections.Generic;

namespace Battleships.Logic
{
    public interface IShipGenerator
    {
        public List<Ship> Ships { get; }
        public void CreateShip(int shipSize);
    }
}