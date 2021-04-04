using System;
using Battleships;
using Xunit;

namespace BattleshipsTests
{
    public class LocationTests
    {
        private const char CharIndex = 'A';
        private const int NumIndex = 1;
        private readonly Location _location;

        public LocationTests()
        {
            _location = new Location(CharIndex, NumIndex);
        }

        [Theory]
        [InlineData('@', 1)]
        [InlineData('K', 1)]
        [InlineData('A', 0)]
        [InlineData('A', 11)]
        [InlineData('@', 0)]
        [InlineData('K', 11)]
        public void Constructor_InvalidValues_ThrowsArgumentOutOfRangeException(char charIndex, int numIndex)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Location(charIndex, numIndex));
        }

        [Fact]
        public void PropertyChar_ReturnsCorrectChar()
        {
            Assert.Equal(CharIndex, _location.Char);
        }

        [Fact]
        public void PropertyNumber_ReturnsCorrectInt()
        {
            Assert.Equal(NumIndex, _location.Number);
        }

        [Fact]
        public void Equals_LocationsWithSameValues_ReturnsCorrectBoolean()
        {
            var anotherLocation = new Location(CharIndex, NumIndex);

            Assert.True(_location == anotherLocation);
            Assert.False(_location != anotherLocation);
            Assert.True(_location.Equals(anotherLocation));
            Assert.True(_location.Equals(anotherLocation as object));
        }

        [Theory]
        [InlineData('A', 2)]
        [InlineData('B', 1)]
        [InlineData('B', 2)]
        public void Equals_LocationsWithDifferentValues_ReturnsCorrectBoolean(char charIndex, int numIndex)
        {
            var anotherLocation = new Location(charIndex, numIndex);

            Assert.True(_location != anotherLocation);
            Assert.False(_location == anotherLocation);
            Assert.False(_location.Equals(anotherLocation));
            Assert.False(_location.Equals(anotherLocation as object));
        }

        [Fact]
        public void Random_ValidValues_ReturnsRandomLocation()
        {
            Assert.IsType<Location>(Location.Random());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("AD")]
        [InlineData("11")]
        public void TryParse_InvalidString_ReturnsFalseOutsDefaultLocationInstance(string s)
        {
            Assert.False(Location.TryParse(s, out var location));
            Assert.True(location == default);
        }

        [Fact]
        public void TryParse_LocationString_ReturnsTrueOutsParsedLocationInstance()
        {
            Assert.True(Location.TryParse("A1", out var location));
            Assert.True(location != default);
        }

        [Fact]
        public void GetHashCode_ReturnsInteger()
        {
            Assert.IsType<int>(_location.GetHashCode());
        }

        [Fact]
        public void ToString_ReturnsJoinedProperties()
        {
            Assert.Equal($"{CharIndex}{NumIndex}", _location.ToString());
        }
    }
}