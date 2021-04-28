using System;
using System.Collections.Generic;
using System.IO;
using Battleships.Logic;
using Moq;
using Xunit;

namespace BattleshipsTests.Logic
{
    public class GameControllerTests
    {
        [Fact]
        public void StartGame_StartsGame_ReturnsVoid()
        {
            var gameMock = new Mock<IGame>();
            gameMock.Setup(s => s.Ships).Returns(new List<Ship>());
            gameMock.Setup(s => s.Shots).Returns(new List<Location>());
            gameMock.Setup(s => s.IsEnded).Returns(true);

            var gameController = new GameController(gameMock.Object);

            var input = new StringReader("A1");
            Console.SetIn(input);

            gameController.StartGame();
        }
    }
}