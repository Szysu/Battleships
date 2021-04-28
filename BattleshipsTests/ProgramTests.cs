using Battleships;
using Battleships.Logic;
using Moq;
using Xunit;

namespace BattleshipsTests
{
    public class ProgramTests
    {
        private readonly Program _instance;

        public ProgramTests()
        {
            var gameController = new Mock<IGameController>().Object;
            var shipGenerator = new Mock<IShipGenerator>().Object;
            _instance = new Program(gameController, shipGenerator);
        }

        [Fact]
        public void StartGame_CorrectlyStartsGame()
        {
            _instance.StartGame();
        }
    }
}