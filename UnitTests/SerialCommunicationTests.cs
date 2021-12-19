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
            string startEndSymbol = SymbolsBase.GetSymbol(SymbolsIDs.startEndMessage);
            var serialPort = new Mock<ISerialPort>();
            serialPort.Setup(x => x.Write(It.IsAny<string>()));
            

            SerialCommunication serialCommunication = new SerialCommunication(serialPort.Object);

            //act
            serialCommunication.SendMessage(message);

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

            serialPort.Setup(x => x.ReadLine()).Returns(SymbolsBase.GetSymbol(SymbolsIDs.startEndMessage) + messageWithoutSymbol + SymbolsBase.GetSymbol(SymbolsIDs.startEndMessage));
            serialPort.Setup(x => x.IsOpen()).Returns(true);
            serialPort.SetupSequence(x => x.BytesToRead())
            .Returns(5) //higher than 0
            .Returns(0);

            //testing object
            var serialCommunication = new SerialCommunication(serialPort.Object);


            //act & assert
            Assert.Equal(messageWithoutSymbol, serialCommunication.ReceiveMessage());
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

            var serialCommunication = new SerialCommunication(serialPort.Object);

            //act & assert
            IncorrectMessageException exception = Assert.Throws<IncorrectMessageException>(() => serialCommunication.ReceiveMessage());
            //TODO: add exception data
            Assert.Equal("aaa", exception.Message);
        }

        [Fact]
        public void ReceiveMessage_SerialPortNotOpen_ThrowException()
        {
            //arrange
            //mock
            var serialPort = new Mock<ISerialPort>();

            serialPort.Setup(x => x.IsOpen()).Returns(false);

            var serialCommunication = new SerialCommunication(serialPort.Object);

            //act & assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => serialCommunication.ReceiveMessage());
            //TODO: add exception data
            Assert.Equal("aaa", exception.Message);
        }
    }
}
