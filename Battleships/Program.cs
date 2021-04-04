using System;
using System.Threading;

namespace Battleships
{
    public class Program
    {
        private static Game _game;

        /// <summary>
        ///     The program input.
        /// </summary>
        public static void Main(string[] args)
        {
            StartGame();
        }

        /// <summary>
        ///     Start the game.
        /// </summary>
        public static void StartGame()
        {
            _game = new Game();

            do
            {
                WriteShipsToDestroy();
                Console.WriteLine(_game.ToString());
                WriteShotLocationRequest();
                var ise = Console.IsOutputRedirected;
                if (!Console.IsOutputRedirected)
                {
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            } while (!_game.IsEnded);

            Console.WriteLine("Thank you for playing!");
        }

        private static void WriteShipsToDestroy()
        {
            Console.WriteLine($"Battleships: {_game.BattleshipsToSink}");
            Console.WriteLine($"Destroyers: {_game.DestroyersToSink}");
        }

        private static void WriteShotLocationRequest()
        {
            Console.Write("Enter the shot's location: ");
            var input = Console.ReadLine();

            if (!Location.TryParse(input, out var location))
            {
                Console.WriteLine("The entered location is invalid.");
                return;
            }

            try
            {
                Console.WriteLine($"The shot {(_game.Shoot(location) ? "hit" : "missed")}!");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("The entered location has been previously shot!");
            }
        }
    }
}