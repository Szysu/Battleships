using System;

namespace Battleships.Logic
{
    public class Playground : IPlayground
    {
        public Playground(int size)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            Size = size;
        }

        public int Size { get; }

        public bool IsValidLocation(Location location)
        {
            if (location == default)
            {
                return false;
            }

            var alphaLessThanMinimum = location.Alpha - 65 < 0;
            var alphaGreaterThanMaximum = location.Alpha - 65 > Size;
            var numberLessThanMinimum = location.Number < 1;
            var numberGreaterThanMaximum = location.Number > Size;

            var alphaIsValid = !alphaLessThanMinimum && !alphaGreaterThanMaximum;
            var numberIsValid = !numberLessThanMinimum && !numberGreaterThanMaximum;

            return alphaIsValid && numberIsValid;
        }
    }
}