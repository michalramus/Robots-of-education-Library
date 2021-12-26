using System;
using Xunit;
using ROELibrary;
using Moq;

namespace UnitTests
{
    public class SerialCommunicationTests
    {
        //SendMessage

        [Theory]
        [InlineData("test")]
        [InlineData("Message")]
        [InlineData("space space")]
        [InlineData("{\"type\": \"config\",\"devTypes\":[\"devx\", \"devy\"],\"devx\": 1,\"devy\": 2,\"devx0\": {\"ID\": 0, \"pins\": [pin1, pin2, pin3, pin4]},\"devy0\": {\"ID\": 0, \"pins\": [pin1, pin2, pin3, pin4]},\"devy1\": {\"ID\": 1, \"pins\": [pin1, pin2, pin3, pin4]},}")]
        [InlineData("{\"type\": \"task\",\"taskNum\": 1,\"task0\": {devType: < devType >, ID: < ID >, task: < task >, valNum: < num >, value1: [valueID, value], value2: [valueID, value]},}")]
        public void SendMessage_SendCorrectMessage_ReturnMessage(string message)
        {
            //arrange
            string startEndSymbol = MessageSymbols.symbols.getValue(EMessageSymbols.startEndMessage);
            var serialPort = new Mock<ISerialPort>();
            serialPort.Setup(x => x.Write(It.IsAny<string>()));

            var messageObject = new Mock<IMessage>();
            messageObject.Setup(x => x.SerializeToJson()).Returns(message);

            SerialCommunication serialCommunication = new SerialCommunication(serialPort.Object);

            //act
            serialCommunication.SendMessage(messageObject.Object);

            //assert
            serialPort.Verify(x => x.Write(startEndSymbol + message + startEndSymbol), Times.Once);
        }



        // ReceiveMessage

        [Theory]
        [InlineData("test")]
        [InlineData("Message")]
        [InlineData("space space")]
        [InlineData("{\"type\": \"config\",\"devTypes\":[\"devx\", \"devy\"],\"devx\": 1,\"devy\": 2,\"devx0\": {\"ID\": 0, \"pins\": [pin1, pin2, pin3, pin4]},\"devy0\": {\"ID\": 0, \"pins\": [pin1, pin2, pin3, pin4]},\"devy1\": {\"ID\": 1, \"pins\": [pin1, pin2, pin3, pin4]},}")]
        [InlineData("{\"type\": \"task\",\"taskNum\": 1,\"task0\": {devType: < devType >, ID: < ID >, task: < task >, valNum: < num >, value1: [valueID, value], value2: [valueID, value]},}")]
        public void ReceiveMessage_ReceiveMessageAndRemoveStartEndMessageSymbol_GetMessage(string messageWithoutSymbol)
        {
            //arrange
            //mock
            var serialPort = new Mock<ISerialPort>();

            serialPort.Setup(x => x.ReadLine()).Returns(MessageSymbols.symbols.getValue(EMessageSymbols.startEndMessage) + messageWithoutSymbol + MessageSymbols.symbols.getValue(EMessageSymbols.startEndMessage));
            serialPort.Setup(x => x.IsOpen()).Returns(true);
            serialPort.SetupSequence(x => x.BytesToRead())
            .Returns(5) //higher than 0
            .Returns(0);

            var messageObject = new Mock<IMessage>();

            //testing object
            var serialCommunication = new SerialCommunication(serialPort.Object);

            //act
            IMessage result = serialCommunication.ReceiveMessage(messageObject.Object);

            //act & assert
            messageObject.Verify(x => x.DeserializeFromJson(messageWithoutSymbol), Times.Once);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("Message")]
        [InlineData("space space")]
        [InlineData("{\"type\": \"config\",\"devTypes\":[\"devx\", \"devy\"],\"devx\": 1,\"devy\": 2,\"devx0\": {\"ID\": 0, \"pins\": [pin1, pin2, pin3, pin4]},\"devy0\": {\"ID\": 0, \"pins\": [pin1, pin2, pin3, pin4]},\"devy1\": {\"ID\": 1, \"pins\": [pin1, pin2, pin3, pin4]},}")]
        [InlineData("{\"type\": \"task\",\"taskNum\": 1,\"task0\": {devType: < devType >, ID: < ID >, task: < task >, valNum: < num >, value1: [valueID, value], value2: [valueID, value]},}")]
        public void ReceiveMessage_ReceiveMessageWithoutStartEndMessageSymbol_ThrowException(string incorrectMessage)
        {
            //arrange
            //mock
            var serialPort = new Mock<ISerialPort>();

            serialPort.Setup(x => x.ReadLine()).Returns(incorrectMessage);
            serialPort.Setup(x => x.IsOpen()).Returns(true);
            serialPort.SetupSequence(x => x.BytesToRead())
            .Returns(5) //higher than 0
            .Returns(0);

            var messageObject = new Mock<IMessage>();

            var serialCommunication = new SerialCommunication(serialPort.Object);

            //act & assert
            IncorrectMessageException exception = Assert.Throws<IncorrectMessageException>(() => serialCommunication.ReceiveMessage(messageObject.Object));
            //TODO: add exception data
            Assert.Equal("Received message is invalid: doesn't have startEndMessage symbol", exception.Message);
            Assert.Equal(incorrectMessage, exception.Data["json"]);
        }

        [Fact]
        public void ReceiveMessage_SerialPortNotOpen_ThrowException()
        {
            //arrange
            //mock
            var serialPort = new Mock<ISerialPort>();

            serialPort.Setup(x => x.IsOpen()).Returns(false);

            var messageObject = new Mock<IMessage>();

            var serialCommunication = new SerialCommunication(serialPort.Object);

            //act & assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => serialCommunication.ReceiveMessage(messageObject.Object));

            Assert.Equal("Serial port is not open", exception.Message);
        }
    }
}
