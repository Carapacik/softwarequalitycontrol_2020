using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace TriangleTests
{
    [TestClass]
    public class TriangleTests
    {
        [TestMethod]
        public void RunTests()
        {
            using var inStream = new StreamReader("intests.txt");
            using var outStream = new StreamWriter("outtests.txt");
            string line;
            bool completedSuccessfully = true;
            int counter = 0;

            while ((line = inStream.ReadLine()) != null)
            {
                var expectedResult = inStream.ReadLine();
                var consoleStr = new StringWriter();
                Console.SetOut(consoleStr);
                Console.SetError(consoleStr);

                Triangle.Program.Main(line.Split(" "));
                if (consoleStr.ToString().Replace("\r\n", "") == expectedResult)
                {
                    outStream.WriteLine($"{counter++} success");
                }
                else
                {
                    outStream.WriteLine($"{counter++} (line: {counter * 2 - 1}) error");
                    completedSuccessfully = false;
                }
            }
            Assert.IsTrue(completedSuccessfully);
        }
    }
}
