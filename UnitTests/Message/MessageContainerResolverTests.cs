using System;
using Xunit;
using ROELibrary;
using Moq;

namespace UnitTests.Msg
{
    public class MessageContainerResolverTests
    {
        [Theory]
        [InlineData((int)EMessageSymbols.msgTypeConfig, typeof(Device))]
        [InlineData((int)EMessageSymbols.msgTypeTask, typeof(Task))]
        [InlineData((int)EMessageSymbols.msgTypeError, typeof(Error))]
        [InlineData((int)EMessageSymbols.msgTypeInformation, typeof(Information))]
        public void GetMessageContainerType_ReturnsCorrectType(int messageType, Type expectedType)
        {
            //act
            var actualType = MessageContainerResolver.GetMessageContainerType((EMessageSymbols)messageType);

            //assert
            Assert.Equal(expectedType, actualType);
        }

        [Fact]
        public void GetMessageContainerType_IncorrectMessageType_throwException()
        {
            //arrange
            var messageType = EMessageSymbols.contTypeTask;

            //act
            var ex = Assert.Throws<IncorrectMessageException>(() => MessageContainerResolver.GetMessageContainerType(messageType));

            //assert
            Assert.Equal("Message type is incorrect", ex.Message);
            Assert.Equal(messageType, ex.Data["messageType"]);
        }
    }
}
