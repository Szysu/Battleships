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
            CreateBattleships(BattleshipsToCreate);
            CreateDestroyers(DestroyersToCreate);

            var playground = new Playground(PlaygroundSize);
            var game = new Game(playground, _shipGenerator.Ships);
            var gameController = new GameController(game);

            gameController.StartGame();
        }

        public static void CreateBattleships(int battleshipsToCreate)
        {
            if (battleshipsToCreate < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i < battleshipsToCreate; i++)
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

        public static void CreateDestroyers(int destroyersToCreate)
        {
            if (destroyersToCreate < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i < destroyersToCreate; i++)
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