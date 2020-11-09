using System;
using Xunit;

namespace MyStringTests
{
    public class MyStringConstructorTests
    {
        [Fact]
        public void Constructor_NoArgs_CreatesEmptyMyString()
        {
            // Arrange + Act
            var myStr = new MyString.MyString();

            // Assert
            Assert.Equal(0, myStr.Length);
            Assert.Equal(new char[0], myStr);
        }

        [Fact]
        public void Constructor_CharArray_CreatesMyStringWithSameCharacters()
        {
            // Arrange
            var chars = new[] {'H', 'e', 'l', 'l', 'o'};

            // Act
            var myStr = new MyString.MyString(chars);

            // Assert
            Assert.Equal(chars.Length, myStr.Length);
            Assert.Equal(chars, myStr);
            Assert.Equal("Hello", myStr);
        }

        [Fact]
        public void Constructor_CharArrayWithNullCharacterAtCenter_CreatesMyStringWithSameCharacters()
        {
            // Arrange
            var chars = new[] {'H', 'e', 'l', 'l', 'o', '\0', 'W', 'o', 'r', 'l', 'd'};

            // Act
            var myStr = new MyString.MyString(chars);

            // Assert
            Assert.Equal(chars.Length, myStr.Length);
            Assert.Equal("Hello\0World", myStr);
        }

        [Fact]
        public void Constructor_CharArrayWithLength_CreatesMyStringWithFirstNChars()
        {
            // Arrange
            const int length = 4;
            var chars = new[] {'H', 'e', 'l', 'l', 'o'};

            // Act
            var myStr = new MyString.MyString(chars, length);

            // Assert
            Assert.Equal(length, myStr.Length);
            Assert.Equal(new[] {'H', 'e', 'l', 'l'}, myStr);
        }

        [Fact]
        public void Constructor_CharArrayWithIncorrectLength_ThrowsException()
        {
            // Arrange
            const int length = 7;
            var chars = new[] {'H', 'e', 'l', 'l', 'o'};

            // Act + Assert
            Assert.Throws<IndexOutOfRangeException>(() => new MyString.MyString(chars, length));
        }

        [Fact]
        public void Constructor_OtherMyStringInstance_CreatesNewInstanceOfMyStringWithSameCharacters()
        {
            // Arrange + Act
            var myStr = new MyString.MyString(new[] {'H', 'e', 'l', 'l', 'o'});
            var copyStr = new MyString.MyString(myStr);

            // Assert
            Assert.NotSame(myStr, copyStr);

            Assert.Equal(myStr.Length, copyStr.Length);
            Assert.Equal(copyStr, myStr);
        }

        [Fact]
        public void Constructor_RegularString_CreatesMyStringWithSameCharacters()
        {
            // Arrange
            const string someString = "Hello";

            // Act
            var myStr = new MyString.MyString(someString);

            // Assert
            Assert.Equal(someString.Length, myStr.Length);
            Assert.Equal(new[] {'H', 'e', 'l', 'l', 'o'}, myStr);
        }
    }
}