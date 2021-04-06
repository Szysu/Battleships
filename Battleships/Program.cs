using System;
using System.IO;
using System.Threading;

namespace Battleships
{
    public static class Program
    {
        private static Game _game;

        /// <summary>
        ///     The program input.
        /// </summary>
        public static void Main(string[] args)
        {
            StartGame();
        }

        private static void StartGame()
        {
            _game = new Game();
            do
            {
                WriteShipsToDestroy();
                Console.WriteLine(_game.ToString());
                WriteShotLocationRequest();
                Thread.Sleep(1000);
                try
                {
                    Console.Clear();
                }
                catch (IOException)
                {
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
            Console.Write("Location to shot: ");
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