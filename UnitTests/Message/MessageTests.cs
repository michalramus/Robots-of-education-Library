using System;
using Xunit;
using ROELibrary;
using Moq;
using Newtonsoft.Json.Linq;

namespace UnitTests.Msg
{
    public class MessageTests
    {
        [Theory]
        [InlineData((int)EMessageSymbols.contTypeDevice, (int)EMessageSymbols.msgTypeConfig)]
        [InlineData((int)EMessageSymbols.contTypeTask, (int)EMessageSymbols.msgTypeTask)]
        [InlineData((int)EMessageSymbols.contTypeError, (int)EMessageSymbols.msgTypeError)]
        [InlineData((int)EMessageSymbols.contTypeInformation, (int)EMessageSymbols.msgTypeInformation)]
        public void addMsgContainer_addContainer_returnCorrectMessageType(int containerType, int messageType)
        {
            //Arrange
            var message = new Message();
            var mockContainer = new Mock<IMessageContainer>();
            mockContainer.Setup(x => x.getContainerType()).Returns((EMessageSymbols)containerType);

            //Act
            message.addMsgContainer(mockContainer.Object);

            //Assert
            Assert.Equal((EMessageSymbols)messageType, message.messageType);
        }

        [Fact]
        public void addMsgContainer_addTwoDifferentContainers_throwException()
        {
            //arrange
            Message message = new Message();
            Mock<IMessageContainer> mockMessageContainer1 = new Mock<IMessageContainer>();
            mockMessageContainer1.Setup(x => x.getContainerType()).Returns(EMessageSymbols.contTypeDevice);
            Mock<IMessageContainer> mockMessageContainer2 = new Mock<IMessageContainer>();
            mockMessageContainer2.Setup(x => x.getContainerType()).Returns(EMessageSymbols.contTypeTask);

            //act & assert
            message.addMsgContainer(mockMessageContainer1.Object);

            var ex = Assert.Throws<IncorrectMessageObjectSetupException>(() => message.addMsgContainer(mockMessageContainer2.Object));
            Assert.Equal("Trying to add message container of different type than first added message container", ex.Message);
            Assert.Equal(EMessageSymbols.msgTypeConfig, ex.Data["messageType"]);
            Assert.Equal(EMessageSymbols.contTypeTask, ex.Data["addedContainerType"]);
        }

        [Theory]
        //config
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"car\",\"ID\":0,\"pins\":[1,15,7,42,6]}]}", "{\"devType\":\"car\",\"ID\":0,\"pins\":[1,15,7,42,6]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"differentRobot\",\"ID\":15,\"pins\":[18,1,30]}]}", "{\"devType\":\"differentRobot\",\"ID\":15,\"pins\":[18,1,30]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"robot\",\"ID\":7843,\"pins\":[14,15]}]}", "{\"devType\":\"robot\",\"ID\":7843,\"pins\":[14,15]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"device\",\"ID\":1008,\"pins\":[1,15,6,7,8,9,10]}]}", "{\"devType\":\"device\",\"ID\":1008,\"pins\":[1,15,6,7,8,9,10]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"arm\",\"ID\":78776,\"pins\":[1,2,3,4,5]}]}", "{\"devType\":\"arm\",\"ID\":78776,\"pins\":[1,2,3,4,5]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"speaker\",\"ID\":4341,\"pins\":[7,14,85,61]}]}", "{\"devType\":\"speaker\",\"ID\":4341,\"pins\":[7,14,85,61]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"driver\",\"ID\":89,\"pins\":[1,3,2,4,5]}]}", "{\"devType\":\"driver\",\"ID\":89,\"pins\":[1,3,2,4,5]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"cooker\",\"ID\":2,\"pins\":[6,16,44,42,3]}]}", "{\"devType\":\"cooker\",\"ID\":2,\"pins\":[6,16,44,42,3]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"car\",\"ID\":10,\"pins\":[8,44,52,63,70]}]}", "{\"devType\":\"car\",\"ID\":10,\"pins\":[8,44,52,63,70]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"robot\",\"ID\":71,\"pins\":[44,15,6,1,2]}]}", "{\"devType\":\"robot\",\"ID\":71,\"pins\":[44,15,6,1,2]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"arm\",\"ID\":898076,\"pins\":[7,18,17,16,15]}]}", "{\"devType\":\"arm\",\"ID\":898076,\"pins\":[7,18,17,16,15]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"device\",\"ID\":554,\"pins\":[4,2,1,6]}]}", "{\"devType\":\"device\",\"ID\":554,\"pins\":[4,2,1,6]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"driver\",\"ID\":233,\"pins\":[5]}]}", "{\"devType\":\"driver\",\"ID\":233,\"pins\":[5]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"differentDevice\",\"ID\":9086,\"pins\":[88,45,32]}]}", "{\"devType\":\"differentDevice\",\"ID\":9086,\"pins\":[88,45,32]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"car\",\"ID\":33,\"pins\":[3,20,31,42]}]}", "{\"devType\":\"car\",\"ID\":33,\"pins\":[3,20,31,42]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"speaker\",\"ID\":0,\"pins\":[7,77,14,91,22,37,14]}]}", "{\"devType\":\"speaker\",\"ID\":0,\"pins\":[7,77,14,91,22,37,14]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"device\",\"ID\":554,\"pins\":[4,2,1,6]},{\"devType\":\"driver\",\"ID\":233,\"pins\":[5]}]}", "{\"devType\":\"device\",\"ID\":554,\"pins\":[4,2,1,6]}", "{\"devType\":\"driver\",\"ID\":233,\"pins\":[5]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"differentDevice\",\"ID\":9086,\"pins\":[88,45,32]},{\"devType\":\"car\",\"ID\":33,\"pins\":[3,20,31,42]},{\"devType\":\"speaker\",\"ID\":0,\"pins\":[7,77,14,91,22,37,14]}]}", "{\"devType\":\"differentDevice\",\"ID\":9086,\"pins\":[88,45,32]}", "{\"devType\":\"car\",\"ID\":33,\"pins\":[3,20,31,42]}", "{\"devType\":\"speaker\",\"ID\":0,\"pins\":[7,77,14,91,22,37,14]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"device\",\"ID\":1008,\"pins\":[1,15,6,7,8,9,10]},{\"devType\":\"arm\",\"ID\":78776,\"pins\":[1,2,3,4,5]}]}", "{\"devType\":\"device\",\"ID\":1008,\"pins\":[1,15,6,7,8,9,10]}", "{\"devType\":\"arm\",\"ID\":78776,\"pins\":[1,2,3,4,5]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"driver\",\"ID\":89,\"pins\":[1,3,2,4,5]},{\"devType\":\"cooker\",\"ID\":2,\"pins\":[6,16,44,42,3]},{\"devType\":\"car\",\"ID\":10,\"pins\":[8,44,52,63,70]}]}", "{\"devType\":\"driver\",\"ID\":89,\"pins\":[1,3,2,4,5]}", "{\"devType\":\"cooker\",\"ID\":2,\"pins\":[6,16,44,42,3]}", "{\"devType\":\"car\",\"ID\":10,\"pins\":[8,44,52,63,70]}")]
        [InlineData((int)EMessageSymbols.contTypeDevice, "{\"type\":\"conf\",\"dev\":[{\"devType\":\"speaker\",\"ID\":4341,\"pins\":[7,14,85,61]},{\"devType\":\"arm\",\"ID\":78776,\"pins\":[1,2,3,4,5]}]}", "{\"devType\":\"speaker\",\"ID\":4341,\"pins\":[7,14,85,61]}", "{\"devType\":\"arm\",\"ID\":78776,\"pins\":[1,2,3,4,5]}")]
        //task
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":91,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarDirec\",\"35\"],[\"vCarDist\",\"25\"]]}]}", "{\"devType\":\"car\",\"ID\":91,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarDirec\",\"35\"],[\"vCarDist\",\"25\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":89,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}]}", "{\"devType\":\"car\",\"ID\":89,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":17,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarGoTime\",\"11\"],[\"vCarRotSpeed\",\"83\"]]}]}", "{\"devType\":\"car\",\"ID\":17,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarGoTime\",\"11\"],[\"vCarRotSpeed\",\"83\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":58,\"task\":\"tCarTurn\",\"exValNum\":4,\"exVal\":[[\"vCarDist\",\"15\"],[\"vCarSpeed\",\"60\"],[\"vCarGoTime\",\"58\"],[\"vCarDirec\",\"8\"]]}]}", "{\"devType\":\"car\",\"ID\":58,\"task\":\"tCarTurn\",\"exValNum\":4,\"exVal\":[[\"vCarDist\",\"15\"],[\"vCarSpeed\",\"60\"],[\"vCarGoTime\",\"58\"],[\"vCarDirec\",\"8\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":22,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}]}", "{\"devType\":\"car\",\"ID\":22,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":93,\"task\":\"tCarGo\",\"exValNum\":3,\"exVal\":[[\"vCarTurnTime\",\"61\"],[\"vCarDist\",\"26\"],[\"vCarSpeed\",\"1\"]]}]}", "{\"devType\":\"car\",\"ID\":93,\"task\":\"tCarGo\",\"exValNum\":3,\"exVal\":[[\"vCarTurnTime\",\"61\"],[\"vCarDist\",\"26\"],[\"vCarSpeed\",\"1\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":39,\"task\":\"tCarGo\",\"exValNum\":1,\"exVal\":[[\"vCarRotSpeed\",\"5\"]]}]}", "{\"devType\":\"car\",\"ID\":39,\"task\":\"tCarGo\",\"exValNum\":1,\"exVal\":[[\"vCarRotSpeed\",\"5\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":72,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarSpeed\",\"95\"],[\"vCarGoTime\",\"17\"]]}]}", "{\"devType\":\"car\",\"ID\":72,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarSpeed\",\"95\"],[\"vCarGoTime\",\"17\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":90,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarGoTime\",\"9\"],[\"vCarDist\",\"2\"]]}]}", "{\"devType\":\"car\",\"ID\":90,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarGoTime\",\"9\"],[\"vCarDist\",\"2\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":5,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}]}", "{\"devType\":\"car\",\"ID\":5,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":72,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarGoTime\",\"8\"],[\"vCarDist\",\"91\"]]}]}", "{\"devType\":\"car\",\"ID\":72,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarGoTime\",\"8\"],[\"vCarDist\",\"91\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":95,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"86\"],[\"vCarDirec\",\"29\"]]}]}", "{\"devType\":\"car\",\"ID\":95,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"86\"],[\"vCarDirec\",\"29\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":73,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarSpeed\",\"97\"],[\"vCarGoTime\",\"53\"]]}]}", "{\"devType\":\"car\",\"ID\":73,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarSpeed\",\"97\"],[\"vCarGoTime\",\"53\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarTurn\",\"exValNum\":1,\"exVal\":[[\"vCarDist\",\"18\"]]}]}", "{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarTurn\",\"exValNum\":1,\"exVal\":[[\"vCarDist\",\"18\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":43,\"task\":\"tCarGo\",\"exValNum\":4,\"exVal\":[[\"vCarGoTime\",\"17\"],[\"vCarSpeed\",\"66\"],[\"vCarRotSpeed\",\"94\"],[\"vCarTurnTime\",\"63\"]]}]}", "{\"devType\":\"car\",\"ID\":43,\"task\":\"tCarGo\",\"exValNum\":4,\"exVal\":[[\"vCarGoTime\",\"17\"],[\"vCarSpeed\",\"66\"],[\"vCarRotSpeed\",\"94\"],[\"vCarTurnTime\",\"63\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":5,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]},{\"devType\":\"car\",\"ID\":72,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarGoTime\",\"8\"],[\"vCarDist\",\"91\"]]}]}", "{\"devType\":\"car\",\"ID\":5,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}", "{\"devType\":\"car\",\"ID\":72,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarGoTime\",\"8\"],[\"vCarDist\",\"91\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":73,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarSpeed\",\"97\"],[\"vCarGoTime\",\"53\"]]},{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarTurn\",\"exValNum\":1,\"exVal\":[[\"vCarDist\",\"18\"]]},{\"devType\":\"car\",\"ID\":22,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}]}", "{\"devType\":\"car\",\"ID\":73,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarSpeed\",\"97\"],[\"vCarGoTime\",\"53\"]]}", "{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarTurn\",\"exValNum\":1,\"exVal\":[[\"vCarDist\",\"18\"]]}", "{\"devType\":\"car\",\"ID\":22,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":95,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"86\"],[\"vCarDirec\",\"29\"]]},{\"devType\":\"car\",\"ID\":93,\"task\":\"tCarGo\",\"exValNum\":3,\"exVal\":[[\"vCarTurnTime\",\"61\"],[\"vCarDist\",\"26\"],[\"vCarSpeed\",\"1\"]]}]}", "{\"devType\":\"car\",\"ID\":95,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"86\"],[\"vCarDirec\",\"29\"]]}", "{\"devType\":\"car\",\"ID\":93,\"task\":\"tCarGo\",\"exValNum\":3,\"exVal\":[[\"vCarTurnTime\",\"61\"],[\"vCarDist\",\"26\"],[\"vCarSpeed\",\"1\"]]}")]
        [InlineData((int)EMessageSymbols.contTypeTask, "{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":43,\"task\":\"tCarGo\",\"exValNum\":4,\"exVal\":[[\"vCarGoTime\",\"17\"],[\"vCarSpeed\",\"66\"],[\"vCarRotSpeed\",\"94\"],[\"vCarTurnTime\",\"63\"]]},{\"devType\":\"car\",\"ID\":58,\"task\":\"tCarTurn\",\"exValNum\":4,\"exVal\":[[\"vCarDist\",\"15\"],[\"vCarSpeed\",\"60\"],[\"vCarGoTime\",\"58\"],[\"vCarDirec\",\"8\"]]}]}", "{\"devType\":\"car\",\"ID\":43,\"task\":\"tCarGo\",\"exValNum\":4,\"exVal\":[[\"vCarGoTime\",\"17\"],[\"vCarSpeed\",\"66\"],[\"vCarRotSpeed\",\"94\"],[\"vCarTurnTime\",\"63\"]]}", "{\"devType\":\"car\",\"ID\":58,\"task\":\"tCarTurn\",\"exValNum\":4,\"exVal\":[[\"vCarDist\",\"15\"],[\"vCarSpeed\",\"60\"],[\"vCarGoTime\",\"58\"],[\"vCarDirec\",\"8\"]]}")]
        public void serializeToJson_serializeCorrectContainerToSend_returnCorrectJson(int containerType, string exceptedJson, params string[] containerJsons)
        {
            //arrange & act
            var message = new Message();

            foreach (string containerJson in containerJsons)
            {
                var mockMessageContainer = new Mock<IMessageContainerToSend>();
                mockMessageContainer.Setup(x => x.getContainerType()).Returns((EMessageSymbols)containerType);
                JObject jObject = JObject.Parse(containerJson);

                mockMessageContainer.Setup(x => x.serializeToJsonObject()).Returns(jObject);
                message.addMsgContainer(mockMessageContainer.Object);
            }

            //assert
            Assert.Equal(exceptedJson, message.SerializeToJson());
        }

        [Theory]//TODO: add tests
        [InlineData("{\"type\":\"info\",\"data\":[[\"firmVer\",\"get\"]]}" ,"[[\"firmVer\",\"get\"]]")]
        public void serializeToJson_serializeInformationAsContainerToSendArray_returnCorrectJson(string exceptedJson, params string[] containerJsons)
        {
            //arrange & act
            var message = new Message();

            foreach (string containerJson in containerJsons)
            {
                var mockMessageContainer = new Mock<IMessageContainerToSendArray>();
                mockMessageContainer.Setup(x => x.getContainerType()).Returns(EMessageSymbols.contTypeInformation);
                JArray jArray = JArray.Parse(containerJson);

                mockMessageContainer.Setup(x => x.serializeToJsonArray()).Returns(jArray);
                message.addMsgContainer(mockMessageContainer.Object);
            }

            //assert
            Assert.Equal(exceptedJson, message.SerializeToJson());
        }

        [Fact]
        public void serializeToJson_serializeNonSerializableContainer_throwException()
        {
            //arrange
            var message = new Message();
            var mockMessageContainer = new Mock<IMessageContainer>();
            mockMessageContainer.Setup(x => x.getContainerType()).Returns(EMessageSymbols.contTypeError);

            //act
            message.addMsgContainer(mockMessageContainer.Object);

            //assert
            var ex = Assert.Throws<IncorrectMessageObjectSetupException>(() => message.SerializeToJson());
            Assert.Equal("Trying to serialize message that cannot be serialized to Json", ex.Message);
            Assert.Equal(EMessageSymbols.msgTypeError, ex.Data["messageType"]);
        }

        [Fact]
        public void serializeToJson_serializeEmptyMessage_throwException()
        {
            //arrange
            var message = new Message();

            //act & assert
            var ex = Assert.Throws<IncorrectMessageObjectSetupException>(() => message.SerializeToJson());
            Assert.Equal("Trying to serialize empty message object", ex.Message);
        }
    }
}
