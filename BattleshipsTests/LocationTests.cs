using System;
using Battleships.Logic;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class LocationTests
    {
        [Fact]
        public void Constructor_Empty_ReturnsVoid()
        {
            new Location();
        }

        [Fact]
        public void Constructor_ValidValues_ReturnsVoid()
        {
            var location = new Location('A', 1);
            location.Alpha.Should().Be('A');
            location.Number.Should().Be(1);
        }

        [Fact]
        public void Constructor_InvalidChar_ThrowsArgumentException()
        {
            Action action = () => new Location('|', 1);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Constructor_InvalidNumber_ThrowsArgumentException()
        {
            Action action = () => new Location('A', -1);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Parse_ValidString_ReturnsCorrectLocationInstance()
        {
            const string str = "A1";
            var location = Location.Parse(str);
            var expectedLocation = new Location('A', 1);
            location.Should().BeEquivalentTo(expectedLocation);
        }

        [Fact]
        public void Parse_InvalidString_ThrowsArgumentException()
        {
            const string str = "8E";
            Action action = () => Location.Parse(str);
            action.Should().Throw<ArgumentException>();
        }
    }
}