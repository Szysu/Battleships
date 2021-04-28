using System;
using Battleships.Logic;

namespace Battleships
{
    public class Program
    {
        public static readonly int PlaygroundSize = 10;
        public static readonly int BattleshipsToCreate = 1;
        public static readonly int DestroyersToCreate = 2;
        public static readonly int BattleshipSize = 5;
        public static readonly int DestroyerSize = 4;

        private readonly IGameController _gameController;
        private readonly IShipGenerator _shipGenerator;

        public Program(IGameController gameController, IShipGenerator shipGenerator)
        {
            _gameController = gameController;
            _shipGenerator = shipGenerator;
        }

        public void StartGame()
        {
            CreateBattleships(BattleshipsToCreate);
            CreateDestroyers(DestroyersToCreate);

            _gameController.StartGame();
        }

        public static void Main(string[] args)
        {
            var shipGenerator = new ShipGenerator(PlaygroundSize);
            var playground = new Playground(PlaygroundSize);

            var game = new Game(playground, shipGenerator.Ships);
            var gameController = new GameController(game);

            var program = new Program(gameController, shipGenerator);
            program.StartGame();
        }

        private void CreateBattleships(int battleshipsToCreate)
        {
            if (battleshipsToCreate < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(battleshipsToCreate));
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

        private void CreateDestroyers(int destroyersToCreate)
        {
            if (destroyersToCreate < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(destroyersToCreate));
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