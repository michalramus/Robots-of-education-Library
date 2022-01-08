using System.Collections.Generic;
using Xunit;
using ROELibrary;
using System;

namespace UnitTests.Msg
{
    public class MessageContainerResolverTests
    {
        public static IEnumerable<object[]> Data_GetMessageContainerType_ReturnCorrectMessageContainer()
        {
            yield return new object[] { (int)EMessageSymbols.msgTypeConfig, new Device() };
            yield return new object[] { (int)EMessageSymbols.msgTypeTask, new Task() };
            yield return new object[] { (int)EMessageSymbols.msgTypeError, new Error() };
            yield return new object[] { (int)EMessageSymbols.msgTypeInformation, new Information() };
        }
        [Theory]
        [MemberData(nameof(Data_GetMessageContainerType_ReturnCorrectMessageContainer))]
        public void GetMessageContainerType_ReturnsCorrectType(int messageType, object expectedType)
        {
            //act
            var actualType = MessageContainerResolver.GetMessageContainerType((EMessageSymbols)messageType);

            //assert
            Assert.Equal(expectedType.ToString(), actualType().ToString());
        }

        [Fact]
        public void GetMessageContainerType_IncorrectMessageType_throwException()
        {
            //arrange
            var messageType = EMessageSymbols.contTypeTask;

            //act
            var ex = Assert.Throws<ArgumentException>(() => MessageContainerResolver.GetMessageContainerType(messageType));

            //assert
            Assert.Equal("Message type is incorrect", ex.Message);
            Assert.Equal(messageType, ex.Data["messageType"]);
        }
    }
}
