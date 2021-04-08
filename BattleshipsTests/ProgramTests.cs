using System;
using System.IO;
using System.Text;
using Battleships;
using FluentAssertions;
using Xunit;

namespace BattleshipsTests
{
    public class ProgramTests
    {
        [Fact]
        public void Main_CorrectlyRunsProgram()
        {
            // 'G11' and 'A1' added to check the detection of duplicate shots and invalid inputs.
            var allLocations = string.Format("G11{0}A1{0}Z{1}", Environment.NewLine, GetAllLocations());

            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader(allLocations);
            Console.SetIn(input);

            Program.Main(Array.Empty<string>());

            Assert.Contains("The entered location is invalid.", output.ToString());
            Assert.Contains("The entered location has been previously shot!", output.ToString());
            Assert.Contains("Thank you for playing!", output.ToString());
        }

        [Theory]
        [InlineData(-1)]
        public void CreateBattleships_InvalidNumberOfBattleshipsToCreate_ThrowsArgumentOutOfRangeException(
            int numberOfBattleships)
        {
            Action action = () => Program.CreateBattleships(numberOfBattleships);
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(-1)]
        public void CreateDestroyers_InvalidNumberOfDestroyersToCreate_ThrowsArgumentOutOfRangeException(
            int numberOfDestroyers)
        {
            Action action = () => Program.CreateDestroyers(numberOfDestroyers);
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        private string GetAllLocations()
        {
            var allLocations = new StringBuilder();

            for (var i = 'A'; i <= 'J'; i++)
            {
                for (var j = 1; j <= 10; j++)
                {
                    allLocations.AppendLine($"{i}{j}");
                }
            }

            return allLocations.ToString();
        }
    }
}