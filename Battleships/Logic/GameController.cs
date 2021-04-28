using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace Battleships.Logic
{
    public class GameController : IGameController
    {
        private readonly IGame _game;

        public GameController(IGame game)
        {
            _game = game;
        }

        public void StartGame()
        {
            do
            {
                WriteShipToDestroy();
                WritePlayground();
                WriteShotLocationRequest();
                WaitAndClearConsole();
            } while (!_game.IsEnded);

            Console.WriteLine("Thank you for playing!");
        }

        private void WriteShipToDestroy()
        {
            var battleshipCount = _game.Ships.Count(s => s.Locations.Count == 5 && !s.IsSunk);
            var destroyerCount = _game.Ships.Count(s => s.Locations.Count == 4 && !s.IsSunk);

            Console.WriteLine($"Battleships: {battleshipCount}");
            Console.WriteLine($"Destroyers: {destroyerCount}");
        }

        private void WritePlayground()
        {
            var str = new StringBuilder();
            str.AppendLine("  1 2 3 4 5 6 7 8 9 10");

            for (var i = 'A'; i <= 'J'; i++)
            {
                str.Append(i);

                for (var j = 1; j <= 10; j++)
                {
                    var currentLocation = new Location
                    {
                        Alpha = i,
                        Number = j
                    };
                    str.Append($" {GetLocationSymbol(currentLocation)}");
                }

                str.AppendLine();
            }

            Console.WriteLine(str);
        }

        private void WriteShotLocationRequest()
        {
            Console.Write("Location to shot: ");
            var input = Console.ReadLine();

            try
            {
                var location = Location.Parse(input);
                try
                {
                    Console.WriteLine($"The shot {(_game.Shoot(location) ? "hit" : "missed")}!");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("The entered location is invalid.");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("The entered location has been previously shot!");
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("The entered location is invalid.");
            }
        }

        private void WaitAndClearConsole()
        {
            if (Console.IsOutputRedirected)
            {
                return;
            }

            Thread.Sleep(1000);
            Console.Clear();
        }

        private char GetLocationSymbol(Location location)
        {
            var symbol = '#';

            if (_game.Shots.Contains(location))
            {
                symbol = 'O';
            }

            var ship = _game.Ships.SingleOrDefault(s => s.Locations.ContainsKey(location));
            if (ship != default && ship.Locations[location])
            {
                symbol = 'X';
            }

            return symbol;
        }
    }
}