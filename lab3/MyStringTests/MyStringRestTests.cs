using System;
using System.IO;
using Xunit;

namespace MyStringTests
{
    public class MyStringRestTests
    {
        [Fact]
        public void GetStringData_MyStringWithSomeData_ReturnsCharEnumerablePlusNullSymbol()
        {
            // Arrange
            const string someString = "Hello";

            // Act
            var myStr = new MyString.MyString(someString);

            // Assert
            Assert.Equal(someString + "\0", myStr.GetStringData);
        }

        [Fact]
        public void SubString_CorrectRange_ReturnsExpectedSubString()
        {
            // Arrange
            var myStr = new MyString.MyString("Hello World");

            // Act
            var newStr = myStr.SubString(6, 5);

            // Assert
            Assert.Equal(5, newStr.Length);
            Assert.Equal("World", newStr);
        }

        [Fact]
        public void SubString_IncorrectRange_ThrowsException()
        {
            // Arrange
            var myStr = new MyString.MyString("Hello World");

            // Act + Assert
            Assert.Throws<Exception>(() => myStr.SubString(7, 5));
        }


        [Fact]
        public void Clear_NonEmptyString_StringIsEmptyNow()
        {
            // Arrange
            var myStr = new MyString.MyString("Hello");

            // Act
            myStr.Clear();

            // Assert
            Assert.Equal(0, myStr.Length);
            Assert.Equal(string.Empty, myStr);
        }


        [Fact]
        public void WriteToStream_StringWithSomeTextToFileStream_WritesSameTextToTheFile()
        {
            // Arrange
            const string filePath = "temp.txt";
            const string fileText = "Hello";
            var myString = new MyString.MyString("Hello");

            // Act
            using (var fsa = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                myString.WriteToStream(fsa);
            }

            var actualFileText = File.ReadAllText(filePath);
            File.Delete(filePath);

            // Assert
            Assert.Equal(fileText, actualFileText);
        }

        [Fact]
        public void ReadFromStream_FileStreamWithText_StringIsFilledWithTheText()
        {
            // Arrange
            const string filePath = "temp.txt";
            const string fileText = "Hello world and everyone here!";
            File.WriteAllText(filePath, fileText);
            var myString = new MyString.MyString();

            // Act
            using (var fsa = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                myString.ReadFromStream(fsa);
            }

            File.Delete(filePath);

            // Assert
            Assert.Equal(fileText, myString);
        }

        [Fact]
        public void ReadFromStream_FileStreamWithoutText_StringIsNotChanged()
        {
            // Arrange
            const string filePath = "temp.txt";
            const string fileText = "";
            File.WriteAllText(filePath, fileText);
            var myString = new MyString.MyString("Hello");

            // Act
            using (var fsa = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                myString.ReadFromStream(fsa);
            }

            File.Delete(filePath);

            // Assert
            Assert.Equal(5, myString.Length);
            Assert.Equal("Hello", myString);
        }

        [Fact]
        public void ReadFromStream_FileStreamWithWhiteSpaces_StringIsFilledWithWhiteSpaces()
        {
            // Arrange
            const string filePath = "temp.txt";
            const string fileText = "            ";
            File.WriteAllText(filePath, fileText);
            var myString = new MyString.MyString("Hello");

            // Act
            using (var fsa = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                myString.ReadFromStream(fsa);
            }

            File.Delete(filePath);

            // Assert
            Assert.Equal(fileText.Length, myString.Length);
            Assert.Equal(fileText, myString);
        }
    }
}