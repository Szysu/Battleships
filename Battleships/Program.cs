using System;
using Battleships.Logic;

namespace Battleships
{
    public static class Program
    {
        public const int PlaygroundSize = 10;
        public const int BattleshipsToCreate = 1;
        public const int DestroyersToCreate = 2;
        public const int BattleshipSize = 5;
        public const int DestroyerSize = 4;

        private static ShipGenerator _shipGenerator;

        public static void Main(string[] args)
        {
            _shipGenerator = new ShipGenerator(PlaygroundSize);
            CreateBattleships();
            CreateDestroyers();

            var playground = new Playground(PlaygroundSize);
            var game = new Game(playground, _shipGenerator.Ships);
            var gameController = new GameController(game);

            gameController.StartGame();
        }

        private static void CreateBattleships()
        {
            for (var i = 0; i < BattleshipsToCreate; i++)
            {
                try
                {
                    _shipGenerator.CreateShip(BattleshipSize);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("The specified number of battleships could not be generated.");
                }
            }
        }

        private static void CreateDestroyers()
        {
            for (var i = 0; i < DestroyersToCreate; i++)
            {
                try
                {
                    _shipGenerator.CreateShip(DestroyerSize);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("The specified number of destroyers could not be generated.");
                }
            }
        }
    }
}