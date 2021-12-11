using System;
using Xunit;
using ROELibrary;
using Moq;

namespace SerialCommunicationTests
{
    public class SerialCommunicationTests
    {
        [Theory]
        [InlineData("test")]
        [InlineData("Message")]
        [InlineData("space space")]
        [InlineData("{\"type\": \"config\",\"devTypes\":[\"devx\", \"devy\"],\"devx\": 1,\"devy\": 2,\"devx0\": {\"ID\": 0, \"pins\": [pin1, pin2, pin3, pin4]},\"devy0\": {\"ID\": 0, \"pins\": [pin1, pin2, pin3, pin4]},\"devy1\": {\"ID\": 1, \"pins\": [pin1, pin2, pin3, pin4]},}")]
        [InlineData("{\"type\": \"task\",\"taskNum\": 1,\"task0\": {devType: < devType >, ID: < ID >, task: < task >, valNum: < num >, value1: [valueID, value], value2: [valueID, value]},}")]
        public void ReceiveMessage_ReceiveMessageAndRemoveStartEndMessageSymbol_GetMessage(string messageWithoutSymbol)
        {
            //mock
            var serialPort = new Mock<ISerialPort>();

            serialPort.Setup(x => x.ReadLine()).Returns(SymbolsBase.GetSymbol(SymbolsIDs.startEndMessage) + messageWithoutSymbol + SymbolsBase.GetSymbol(SymbolsIDs.startEndMessage));
            serialPort.SetupSequence(x => x.BytesToRead())
            .Returns(5) //higher than 0
            .Returns(0);

            //testing object
            var serialCommunication = new SerialCommunication(serialPort.Object);

            //assert
            Assert.Equal(messageWithoutSymbol, serialCommunication.ReceiveMessage());
        }
    }
}
