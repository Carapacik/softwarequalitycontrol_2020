using System;
using System.IO;
using System.Text.RegularExpressions;
using Triangle;
using Xunit;

namespace TriangleTests
{
    public class TriangleTests
    {
        [Fact]
        public void RunTests()
        {
            using var inStream = new StreamReader("../../../input.txt");
            using var outStream = new StreamWriter("../../../output.txt");
            string line;
            var completedSuccessfully = true;
            var counter = 0;

            while ((line = inStream.ReadLine()) != null)
            {
                var args = Regex.Replace(line, @"\s+", " ").Split();
                var expectedResult = inStream.ReadLine();
                var consoleStr = new StringWriter();
                Console.SetOut(consoleStr);
                Console.SetError(consoleStr);

                Program.Main(args);
                if (consoleStr.ToString().Replace("\r\n", "") == expectedResult)
                {
                    outStream.WriteLine($"{counter++} success");
                }
                else
                {
                    outStream.WriteLine($"{counter++} (line: {counter * 2}) error");
                    completedSuccessfully = false;
                }
            }

            Assert.True(completedSuccessfully);
        }
    }
}