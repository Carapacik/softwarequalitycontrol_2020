using System;
using Xunit;

namespace MyStringTests
{
    public class MyStringOperatorTests
    {
        [Fact]
        public void Assignment_SomeClassInstance_NewInstanceIsSame()
        {
            // Arrange
            var a = new MyString.MyString("hey");

            // Act
            var b = a; // Assignment reference

            // Act
            Assert.Same(a, b);
        }

        [Fact]
        public void Concat_MyStringWithMyString_ReturnsCombinedMyString()
        {
            // Arrange
            var a = new MyString.MyString("hey ");
            var b = new MyString.MyString(new[] {'w', 'o', 'r', 'l', 'd'});

            // Act
            var c = a + b;

            // Assert
            Assert.Equal(a.Length + b.Length, c.Length);
            Assert.Equal("hey world", c);
        }

        [Fact]
        public void Concat_StringWithMyString_ReturnsCombinedMyString()
        {
            // Arrange
            const string a = "hey";
            var b = new MyString.MyString(" world");

            // Act
            var c = a + b;

            // Assert
            Assert.Equal(a.Length + b.Length, c.Length);
            Assert.Equal("hey world", c);
        }

        [Fact]
        public void Concat_CharArrayWithMyString_ReturnsCombinedMyString()
        {
            // Arrange
            var a = new[] {'h', 'e', 'y', ' '};
            var b = new MyString.MyString("world");

            // Act
            var c = a + b;

            // Assert
            Assert.Equal(a.Length + b.Length, c.Length);
            Assert.Equal("hey world", c);
        }

        [Fact]
        public void Comparison_TwoIdenticalStrings_ExpectedOperatorsBehaviour()
        {
            // Arrange
            var a = new MyString.MyString("Hello");
            var b = new MyString.MyString("Hello");

            // Act + Assert
            Assert.True(a == b);
            Assert.False(a != b);

            Assert.True(a >= b);
            Assert.True(a <= b);

            Assert.False(a > b);
            Assert.False(a < b);
        }

        [Fact]
        public void Comparison_TwoDifferentStrings_ExpectedOperatorsBehaviour()
        {
            // Arrange
            var a = new MyString.MyString("Hello");
            var b = new MyString.MyString("Hell");

            // Act + Assert
            Assert.False(a == b);
            Assert.True(a != b);

            Assert.True(a >= b);
            Assert.False(a <= b);

            Assert.True(a > b);
            Assert.False(a < b);
        }

        [Fact]
        public void IndexGet_CorrectIndex_ReturnsExpectedCharacter()
        {
            // Arrange
            var myStr = new MyString.MyString("Hello");

            // Act + Assert
            Assert.Equal('o', myStr[4]);
        }

        [Fact]
        public void IndexGet_IncorrectIndex_ThrowsException()
        {
            // Arrange
            var myStr = new MyString.MyString("Hello");

            // Act + Assert
            Assert.Throws<IndexOutOfRangeException>(() => myStr[-1]);
            Assert.Throws<IndexOutOfRangeException>(() => myStr[5]);
        }

        [Fact]
        public void IndexSet_CorrectIndex_CharAtIndexIsChanged()
        {
            // Arrange
            var myStr = new MyString.MyString("Hello") {[3] = 'a'};

            // Assert
            Assert.Equal('a', myStr[3]);
        }
    }
}