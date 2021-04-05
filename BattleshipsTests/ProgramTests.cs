using System;
using System.IO;
using System.Text;
using Battleships;
using Xunit;

namespace BattleshipsTests
{
    public class ProgramTests
    {
        [Fact]
        public void Main_CorrectlyRunsProgram()
        {
            var allLocations = GetAllLocations();

            var input = new StringReader(allLocations);
            Console.SetIn(input);

            Program.Main(Array.Empty<string>());
        }

        [Fact]
        public void StartGame_CorrectlyStartsGame()
        {
            // 'G11' and 'A1' added to check the detection of duplicate shots and invalid inputs.
            var allLocations = string.Format("G11{0}A1{0}{1}", Environment.NewLine, GetAllLocations());

            var output = new StringWriter();
            Console.SetOut(output);

            var input = new StringReader(allLocations);
            Console.SetIn(input);

            Program.StartGame();

            Assert.Contains("The entered location is invalid.", output.ToString());
            Assert.Contains("The entered location has been previously shot!", output.ToString());
            Assert.Contains("Thank you for playing!", output.ToString());
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