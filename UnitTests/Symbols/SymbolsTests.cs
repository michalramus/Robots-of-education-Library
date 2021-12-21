using System.Collections.Generic;
using Xunit;
using ROELibrary;


namespace UnitTests
{
    public class SymbolsTests
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(15, "key")]
        [InlineData(37, "symbol15 8")]
        [InlineData(1578, "element")]
        [InlineData(1434, "Dictionary")]
        [InlineData(892804, "892804")]
        [InlineData(6, "%$#@")]
        [InlineData(4898, "GG")]
        [InlineData(908, "908")]
        [InlineData(634, "Testing")]
        [InlineData(185, @"\/\/'uu")]
        [InlineData(10, "18")]
        public void getValue_getCorrectValue_returnValue(int key, string value)
        {
            //Arrange
            Symbols<int> symbols = new Symbols<int>(
                new Dictionary<int, string>
                {
                    {key, value},
                    {key + 1, value + 1},
                    {key + 2, value + 2},
                    {key + 3, value + 3},
                    {key + 4, value + 4},
                }
            );

            //Act
            string result = symbols.getValue(key);

            //Assert
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(37)]
        [InlineData(1578)]
        [InlineData(1434)]
        [InlineData(892804)]
        [InlineData(6)]
        [InlineData(4898)]
        [InlineData(908)]
        [InlineData(634)]
        [InlineData(185)]
        [InlineData(10)]
        public void getValue_getIncorrectValue_throwException(int key)
        {
            //Arrange
            Symbols<int> symbols = new Symbols<int>(
                new Dictionary<int, string>
                {
                    {key + 1, key.ToString() + 1},
                    {key + 2, key.ToString() + 2},
                    {key + 3, key.ToString() + 3},
                    {key + 4, key.ToString() + 4},
                }
            );

            //Act & Assert
            KeyNotFoundException result = Assert.Throws<KeyNotFoundException>(() => symbols.getValue(key));

            Assert.Equal($"Key {key} not found", result.Message);
            Assert.Equal(key, result.Data["key"]);
            Assert.Equal(typeof(int), result.Data["Symbol type"]);
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(15, "key")]
        [InlineData(37, "symbol15 8")]
        [InlineData(1578, "element")]
        [InlineData(1434, "Dictionary")]
        [InlineData(892804, "892804")]
        [InlineData(6, "%$#@")]
        [InlineData(4898, "GG")]
        [InlineData(908, "908")]
        [InlineData(634, "Testing")]
        [InlineData(185, @"\/\/'uu")]
        [InlineData(10, "18")]
        public void getKey_getCorrectKey_returnKey(int key, string value)
        {
            //Arrange
            Symbols<int> symbols = new Symbols<int>(
                new Dictionary<int, string>
                {
                    {key, value},
                    {key + 1, value + 1},
                    {key + 2, value + 2},
                    {key + 3, value + 3},
                    {key + 4, value + 4},
                }
            );

            //Act
            int result = symbols.getKey(value);

            //Assert
            Assert.Equal(key, result);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("key")]
        [InlineData("symbol15 8")]
        [InlineData("element")]
        [InlineData("Dictionary")]
        [InlineData("892804")]
        [InlineData("%$#@")]
        [InlineData("GG")]
        [InlineData("908")]
        [InlineData("Testing")]
        [InlineData(@"\/\/'uu")]
        [InlineData("18")]
        public void getKey_getIncorrectKey_throwException(string value)
        {
            //Arrange
            Symbols<int> symbols = new Symbols<int>(
                new Dictionary<int, string>
                {
                    {1, value + 2},
                    {2, value + 3},
                    {3, value + 4},
                    {4, value + 5},
                }
            );

            //Act & Assert
            ValueNotFoundException result = Assert.Throws<ValueNotFoundException>(() => symbols.getKey(value));

            Assert.Equal($"Value {value} not found" , result.Message);
            Assert.Equal(value, result.Data["value"]);
            Assert.Equal(typeof(int), result.Data["Symbol type"]);

        }
    }
}
