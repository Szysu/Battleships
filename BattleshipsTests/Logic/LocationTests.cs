using System;
using Battleships.Logic;
using Xunit;

namespace BattleshipsTests.Logic
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
            Assert.Equal('A', location.Alpha);
            Assert.Equal(1, location.Number);
        }

        [Fact]
        public void Constructor_InvalidChar_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new Location('|', 1); });
        }

        [Fact]
        public void Constructor_InvalidNumber_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => { new Location('A', -1); });
        }

        [Fact]
        public void Parse_ValidString_ReturnsCorrectLocationInstance()
        {
            const string str = "A1";
            var location = Location.Parse(str);
            var expectedLocation = new Location('A', 1);
            Assert.Equal(expectedLocation, location);
        }

        [Fact]
        public void Parse_InvalidString_ThrowsArgumentException()
        {
            const string str = "8E";
            Assert.Throws<ArgumentException>(() => { Location.Parse(str); });
        }
    }
}