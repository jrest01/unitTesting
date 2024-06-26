using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;

namespace StringManipulation.Test

{
    public class StringOperationsTest
    {
        StringOperations strOperation = new StringOperations();
        [Fact]
        public void ConcatenateStrings()
        {
            var result = strOperation.ConcatenateStrings("Hello", "world");
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.Equal("Hello world", result);

        }

        [Fact]
        public void IsPalindrome_True()
        {
            var result = strOperation.IsPalindrome("amor roma");
            Assert.True(result);
        }

        [Theory]
        [InlineData("Hello", "olleH")]
        public void ReverseString(string txt, string rTxt)
        {
            var result = strOperation.ReverseString(txt);
            Assert.Equal(rTxt, result);
        }

        [Fact]
        public void GetStringLength_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => strOperation.GetStringLength(null));
        }

        [Fact]
        public void GetStringLength()
        {
            var result = strOperation.GetStringLength("123456789");
            Assert.Equal(9, result);
        }

        [Fact]
        public void RemoveWhitespace()
        {
            var result = strOperation.RemoveWhitespace("a b c d e");
            Assert.Equal("abcde", result);
        }

        [Fact]
        public void RemoveWhitespace_False()
        {
            var result = strOperation.RemoveWhitespace("a b c d e");
            Assert.NotEqual("abcde ", result);
        }

        [Fact]
        public void TruncateString_Exception()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => strOperation.TruncateString("String", -1));
        }

        [Theory]
        [InlineData("abc", 4, "abc")]
        [InlineData(null, 4, null)]
        [InlineData("abcde", 4, "abcd")]
        public void TruncateStringX(string text, int limit, string expected)
        {
            var result = strOperation.TruncateString(text, limit);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsPalindrome_False()
        {
            var result = strOperation.IsPalindrome("false");
            Assert.False(result);
        }

        [Fact]
        public void CountOccurrences()
        {
            var mockLogger = new Mock<ILogger<StringOperations>>();
            strOperation = new StringOperations(mockLogger.Object);
            var result = strOperation.CountOccurrences("sssaaa", 'a');
            Assert.Equal(3, result);
        }

        [Theory]
        [InlineData("Vaca", "Vacas")]
        [InlineData("Cosa", "Cosas")]
        [InlineData("Tesoro", "Tesoros")]
        public void Pluralize(string input, string expected)
        {
            var result = strOperation.Pluralize(input);
            Assert.Equal(result, expected);
        }

        [Fact]
        public void QuantintyInWords()
        {
            var result = strOperation.QuantintyInWords("gato", 10);
            Assert.StartsWith("ten", result);
            Assert.Contains("gato", result);
        }

        [Theory]
        [InlineData("X", 10)]
        [InlineData("V", 5)]
        [InlineData("III", 3)]
        public void FromRomanToNumber(string romanNumber, int expected)
        {
            var result = strOperation.FromRomanToNumber(romanNumber);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReadFile()
        {
            var mockFileReader = new Mock<IFileReaderConector>();
            mockFileReader.Setup(p => p.ReadString(It.IsAny<string>())).Returns("sometext");
            var result = strOperation.ReadFile(mockFileReader.Object, "fle.txt");

            Assert.Equal("sometext", result);
        }
    }
}
