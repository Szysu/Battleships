using System;
using Battleships.Logic;
using Xunit;

namespace BattleshipsTests.Logic
{
    public class PlaygroundTests
    {
        [Fact]
        public void Constructor_InvalidValue_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { new Playground(0); });
        }

        [Fact]
        public void Constructor_ValidValue_ReturnsVoid()
        {
            new Playground(10);
        }

        [Fact]
        public void IsValidLocation_ValidLocation_ReturnsTrue()
        {
            var playground = new Playground(10);
            var location = new Location
            {
                Alpha = 'A',
                Number = 1
            };
            var result = playground.IsValidLocation(location);
            Assert.True(result);
        }

        [Fact]
        public void IsValidLocation_InvalidLocation_ReturnsFalse()
        {
            var playground = new Playground(2);
            var location = new Location
            {
                Alpha = 'Z',
                Number = 100
            };
            var result = playground.IsValidLocation(location);
            Assert.False(result);
        }

        [Fact]
        public void IsValidLocation_DefaultLocation_ReturnsFalse()
        {
            var playground = new Playground(10);
            var result = playground.IsValidLocation(default);
            Assert.False(result);
        }
    }
}