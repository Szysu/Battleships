using System;
using Battleships.Logic;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class PlaygroundTests
    {
        [Fact]
        public void Constructor_InvalidValue_ThrowsArgumentOutOfRangeException()
        {
            Action action = () => new Playground(0);
            action.Should().Throw<ArgumentOutOfRangeException>();
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
            result.Should().BeTrue();
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
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValidLocation_DefaultLocation_ReturnsFalse()
        {
            var playground = new Playground(10);
            playground.IsValidLocation(default);
        }
    }
}