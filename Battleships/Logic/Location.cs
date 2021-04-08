using System;
using System.Text.RegularExpressions;

namespace Battleships.Logic
{
    public record Location
    {
        public char Alpha { get; init; }
        public int Number { get; init; }

        public static Location Parse(string s)
        {
            s = s.ToUpper();

            var regex = new Regex(@"([A-Z])(\d+)");
            var result = regex.Match(s);

            if (result.Groups.Count != 3)
            {
                throw new ArgumentException("Specified string is not convertable.", nameof(s));
            }

            var alpha = char.Parse(result.Groups[1].Value);
            var number = int.Parse(result.Groups[2].Value);

            return new Location
            {
                Alpha = alpha,
                Number = number
            };
        }
    }
}