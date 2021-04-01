using Xunit;

namespace BattleshipsTests
{
    public class GameTests
    {
        private readonly Game _game;

        public GameTests()
        {
            _game = new Game();
        }

        [Fact]
        public void Constructor_NewInstance()
        {
            var game = new Game();
            Assert.NotNull(game);
        }
    }
}
