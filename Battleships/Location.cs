using System;

namespace Battleships
{
    /// <summary>
    ///     A structure representing a location with a character part and a numeric part.
    /// </summary>
    public readonly struct Location : IEquatable<Location>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="charIndex">A capital letter from 'A' to 'J'.</param>
        /// <param name="numIndex">An integer from 1 to 10.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="charIndex"/> or <paramref name="numIndex"/> is not valid.
        /// </exception>
        public Location(char charIndex, int numIndex)
        {
            if (charIndex < 'A' || charIndex > 'J')
                throw new ArgumentOutOfRangeException(nameof(charIndex));

            if (numIndex < 1 || numIndex > 10)
                throw new ArgumentOutOfRangeException(nameof(numIndex));

            Char = charIndex;
            Number = numIndex;
        }

        /// <value>Gets the number part.</value>
        public int Number { get; }

        /// <value>Gets the character part.</value>
        public char Char { get; }

        /// <summary>
        ///     Indicates whether <paramref name="a"/> and <paramref name="b"/> have the same
        ///     properties values.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="a"/> and <paramref name="b"/> have the
        ///     same properties values, otherwise <see langword="false"/>.
        /// </returns>
        public static bool operator ==(Location a, Location b)
            => a.Char == b.Char && a.Number == b.Number;

        /// <summary>
        ///     Indicates whether <paramref name="a"/> and <paramref name="b"/> have a different
        ///     properties values.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="a"/> and <paramref name="b"/> have a
        ///     different properties values, otherwise <see langword="false"/>.
        /// </returns>
        public static bool operator !=(Location a, Location b)
            => !(a == b);

        /// <summary>
        ///     Returns a new instance of the <see cref="Location"/> class with random values.
        /// </summary>
        /// <param name="xOffset">The offset from the last correct X-coordinate.</param>
        /// <param name="yOffset">The offset from the last correct Y-coordinate.</param>
        /// <returns>A new instance of <see cref="Location"/> class with random values.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     <paramref name="xOffset"/> or <paramref name="yOffset"/> is invalid.
        /// </exception>
        public static Location Random(int xOffset = 0, int yOffset = 0)
        {
            var random = new Random();
            var randomChar = (char)random.Next('A', 'K' - xOffset);
            var randomNumber = random.Next(1, 11 - yOffset);
            return new Location(randomChar, randomNumber);
        }

        /// <summary>
        ///     Converts the string representation of a location to its <see cref="Location"/>
        ///     instance equivalent.
        /// </summary>
        /// <param name="s">A string to convert.</param>
        /// <param name="result">
        ///     If conversion succeeded contains converted <paramref name="s"/>, otherwise contains
        ///     default instance of the <see cref="Location"/> class.
        /// </param>
        /// <returns><see langword="true"/> if conversion succeeded, otherwise <see langword="false"/>.</returns>
        public static bool TryParse(string s, out Location result)
        {
            result = default;

            if (string.IsNullOrWhiteSpace(s))
                return false;

            try
            {
                var charIndex = s[0];
                var numIndex = int.Parse(s[1..]);
                result = new Location(charIndex, numIndex);
                return true;
            }
            catch (Exception e) when (e is ArgumentNullException || e is FormatException || e is OverflowException || e is ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        /// <summary>
        ///     Indicates whether <see langword="this"/> instance and a specified object are equal.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        ///     <see langword="true"/> if the current object is equal to the <paramref
        ///     name="other"/> parameter; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Location other)
            => this == other;

        /// <summary>
        ///     Indicates whether <see langword="this"/> instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns>
        ///     <see langword="true"/> if <paramref name="obj"/> and this instance are the same type
        ///     and represent the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj)
            => obj is Location other && Equals(other);

        /// <summary>
        ///     Returns the hash code for <see langword="this"/> instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
            => HashCode.Combine(Number, Char);

        /// <summary>
        ///     Returns joined <see cref="Char"/> and <see cref="Number"/> properties.
        /// </summary>
        /// <returns>Joined <see cref="Char"/> and <see cref="Number"/> properties.</returns>
        public override string ToString()
            => $"{Char}{Number}";
    }
}