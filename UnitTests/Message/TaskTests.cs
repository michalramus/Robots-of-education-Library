using Xunit;
using ROELibrary;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace UnitTests.Msg
{
    public class TaskTests
    {
        [Fact]
        public void AddExtraValue_AddCorrectValues_GetCorrectValuesAndValuesNumber()
        {
            //arrange
            Task task = new Task();

            //act
            task.AddExtraValue((ERobotsSymbols)1, "value");
            task.AddExtraValue((ERobotsSymbols)3, "value2");
            task.AddExtraValue((ERobotsSymbols)5, "value3");

            //assert
            Assert.Equal(3, task.extraValuesNumber);

            Assert.Equal("value", task.extraValues[(ERobotsSymbols)1]);
            Assert.Equal("value2", task.extraValues[(ERobotsSymbols)3]);
            Assert.Equal("value3", task.extraValues[(ERobotsSymbols)5]);
        }

        public static IEnumerable<object[]> Data_SerializeToJsonObject_SerializeTask_ReturnsCorrectJson()
        {
            yield return new object[] { (int)ERobotsSymbols.car, 91, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarAngle, "35" }, { (int)ERobotsSymbols.valCarDistance, "25" }, }, "{\"devType\":\"car\",\"ID\":91,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarAngle\",\"35\"],[\"vCarDist\",\"25\"]]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 89, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { }, "{\"devType\":\"car\",\"ID\":89,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 17, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "11" }, { (int)ERobotsSymbols.valCarRotationalSpeed, "83" }, }, "{\"devType\":\"car\",\"ID\":17,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarGoTime\",\"11\"],[\"vCarRotSpeed\",\"83\"]]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 58, (int)ERobotsSymbols.taskCarTurn, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarDistance, "15" }, { (int)ERobotsSymbols.valCarSpeed, "60" }, { (int)ERobotsSymbols.valCarGoTime, "58" }, { (int)ERobotsSymbols.valCarAngle, "8" }, }, "{\"devType\":\"car\",\"ID\":58,\"task\":\"tCarTurn\",\"exValNum\":4,\"exVal\":[[\"vCarDist\",\"15\"],[\"vCarSpeed\",\"60\"],[\"vCarGoTime\",\"58\"],[\"vCarAngle\",\"8\"]]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 22, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { }, "{\"devType\":\"car\",\"ID\":22,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 93, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarTurnTime, "61" }, { (int)ERobotsSymbols.valCarDistance, "26" }, { (int)ERobotsSymbols.valCarSpeed, "1" }, }, "{\"devType\":\"car\",\"ID\":93,\"task\":\"tCarGo\",\"exValNum\":3,\"exVal\":[[\"vCarTurnTime\",\"61\"],[\"vCarDist\",\"26\"],[\"vCarSpeed\",\"1\"]]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 39, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarRotationalSpeed, "5" }, }, "{\"devType\":\"car\",\"ID\":39,\"task\":\"tCarGo\",\"exValNum\":1,\"exVal\":[[\"vCarRotSpeed\",\"5\"]]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 72, (int)ERobotsSymbols.taskCarTurn, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarSpeed, "95" }, { (int)ERobotsSymbols.valCarGoTime, "17" }, }, "{\"devType\":\"car\",\"ID\":72,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarSpeed\",\"95\"],[\"vCarGoTime\",\"17\"]]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 90, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "9" }, { (int)ERobotsSymbols.valCarDistance, "2" }, }, "{\"devType\":\"car\",\"ID\":90,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarGoTime\",\"9\"],[\"vCarDist\",\"2\"]]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 5, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { }, "{\"devType\":\"car\",\"ID\":5,\"task\":\"tCarGo\",\"exValNum\":0,\"exVal\":[]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 72, (int)ERobotsSymbols.taskCarTurn, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "8" }, { (int)ERobotsSymbols.valCarDistance, "91" }, }, "{\"devType\":\"car\",\"ID\":72,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarGoTime\",\"8\"],[\"vCarDist\",\"91\"]]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 95, (int)ERobotsSymbols.taskCarTurn, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarDistance, "86" }, { (int)ERobotsSymbols.valCarAngle, "29" }, }, "{\"devType\":\"car\",\"ID\":95,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"86\"],[\"vCarAngle\",\"29\"]]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 73, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarSpeed, "97" }, { (int)ERobotsSymbols.valCarGoTime, "53" }, }, "{\"devType\":\"car\",\"ID\":73,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarSpeed\",\"97\"],[\"vCarGoTime\",\"53\"]]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 1, (int)ERobotsSymbols.taskCarTurn, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarDistance, "18" }, }, "{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarTurn\",\"exValNum\":1,\"exVal\":[[\"vCarDist\",\"18\"]]}" };
            yield return new object[] { (int)ERobotsSymbols.car, 43, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "17" }, { (int)ERobotsSymbols.valCarSpeed, "66" }, { (int)ERobotsSymbols.valCarRotationalSpeed, "94" }, { (int)ERobotsSymbols.valCarTurnTime, "63" }, }, "{\"devType\":\"car\",\"ID\":43,\"task\":\"tCarGo\",\"exValNum\":4,\"exVal\":[[\"vCarGoTime\",\"17\"],[\"vCarSpeed\",\"66\"],[\"vCarRotSpeed\",\"94\"],[\"vCarTurnTime\",\"63\"]]}" };
        }
        [Theory]
        [MemberData(nameof(Data_SerializeToJsonObject_SerializeTask_ReturnsCorrectJson))]
        public void SerializeToJsonObject_SerializeTask_ReturnsCorrectJson(int devType, uint devId, int task, Dictionary<int, string> extraValues, string Json)
        {
            //arrange
            Task taskObj = new Task();

            taskObj.devID = devId;
            taskObj.devType = (ERobotsSymbols)devType;
            taskObj.task = (ERobotsSymbols)task;

            foreach (KeyValuePair<int, string> extraValue in extraValues)
            {
                taskObj.AddExtraValue((ERobotsSymbols)extraValue.Key, extraValue.Value);
            }

            //act
            JObject jsonObject = taskObj.serializeToJsonObject();
            string result = jsonObject.ToString();

            result = result.Replace("\n", "");
            result = result.Replace(" ", "");
            result = result.Replace("\r", "");

            //assert
            Assert.Equal(Json, result);
        }


        public static IEnumerable<object[]> Data_SerializeToJsonObject_NotSetDeviceID_ThrowException()
        {
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarAngle, "35" }, { (int)ERobotsSymbols.valCarDistance, "25" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { } };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "11" }, { (int)ERobotsSymbols.valCarRotationalSpeed, "83" }, } };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarTurn, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarDistance, "15" }, { (int)ERobotsSymbols.valCarSpeed, "60" }, { (int)ERobotsSymbols.valCarGoTime, "58" }, { (int)ERobotsSymbols.valCarAngle, "8" }, } };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { }, };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarTurnTime, "61" }, { (int)ERobotsSymbols.valCarDistance, "26" }, { (int)ERobotsSymbols.valCarSpeed, "1" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarRotationalSpeed, "5" }, } };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarTurn, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarSpeed, "95" }, { (int)ERobotsSymbols.valCarGoTime, "17" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "9" }, { (int)ERobotsSymbols.valCarDistance, "2" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { }, };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarTurn, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "8" }, { (int)ERobotsSymbols.valCarDistance, "91" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarTurn, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarDistance, "86" }, { (int)ERobotsSymbols.valCarAngle, "29" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarSpeed, "97" }, { (int)ERobotsSymbols.valCarGoTime, "53" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarTurn, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarDistance, "18" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, (int)ERobotsSymbols.taskCarGo, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "17" }, { (int)ERobotsSymbols.valCarSpeed, "66" }, { (int)ERobotsSymbols.valCarRotationalSpeed, "94" }, { (int)ERobotsSymbols.valCarTurnTime, "63" }, }, };
        }
        [Theory]
        [MemberData(nameof(Data_SerializeToJsonObject_NotSetDeviceID_ThrowException))]
        public void SerializeToJsonObject_NotSetDeviceID_ThrowException(int devType, int task, Dictionary<int, string> extraValues)
        {
            //arrange
            Task taskObj = new Task();
            Dictionary<ERobotsSymbols, string> extraValuesDict = new Dictionary<ERobotsSymbols, string>(); // rewrite extraValues dictionary to change type of key to ERobotsSymbols

            taskObj.devType = (ERobotsSymbols)devType;
            taskObj.task = (ERobotsSymbols)task;

            foreach (KeyValuePair<int, string> extraValue in extraValues)
            {
                taskObj.AddExtraValue((ERobotsSymbols)extraValue.Key, extraValue.Value);
                extraValuesDict.Add((ERobotsSymbols)extraValue.Key, extraValue.Value);
            }

            //prepare extraValues for assert
            string extraValuesResult = string.Join("", extraValuesDict);


            //act & assert
            MsgContainerNotSetException ex = Assert.Throws<MsgContainerNotSetException>(() => taskObj.serializeToJsonObject());

            Assert.Equal("Task container is not correctly set", ex.Message);

            Assert.Null(ex.Data["devID"]);
            Assert.Equal((ERobotsSymbols)devType, ex.Data["devType"]);
            Assert.Equal((ERobotsSymbols)task, ex.Data["task"]);
            Assert.Equal(extraValuesResult, ex.Data["extraValues"]);
        }

        public static IEnumerable<object[]> Data_SerializeToJsonObject_NotSetTask_ThrowException()
        {
            yield return new object[] { (int)ERobotsSymbols.car, 91, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarAngle, "35" }, { (int)ERobotsSymbols.valCarDistance, "25" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, 89, new Dictionary<int, string> { } };
            yield return new object[] { (int)ERobotsSymbols.car, 17, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "11" }, { (int)ERobotsSymbols.valCarRotationalSpeed, "83" }, } };
            yield return new object[] { (int)ERobotsSymbols.car, 58, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarDistance, "15" }, { (int)ERobotsSymbols.valCarSpeed, "60" }, { (int)ERobotsSymbols.valCarGoTime, "58" }, { (int)ERobotsSymbols.valCarAngle, "8" }, } };
            yield return new object[] { (int)ERobotsSymbols.car, 22, new Dictionary<int, string> { }, };
            yield return new object[] { (int)ERobotsSymbols.car, 93, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarTurnTime, "61" }, { (int)ERobotsSymbols.valCarDistance, "26" }, { (int)ERobotsSymbols.valCarSpeed, "1" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, 39, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarRotationalSpeed, "5" }, } };
            yield return new object[] { (int)ERobotsSymbols.car, 72, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarSpeed, "95" }, { (int)ERobotsSymbols.valCarGoTime, "17" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, 90, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "9" }, { (int)ERobotsSymbols.valCarDistance, "2" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, 5, new Dictionary<int, string> { }, };
            yield return new object[] { (int)ERobotsSymbols.car, 72, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "8" }, { (int)ERobotsSymbols.valCarDistance, "91" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, 95, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarDistance, "86" }, { (int)ERobotsSymbols.valCarAngle, "29" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, 73, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarSpeed, "97" }, { (int)ERobotsSymbols.valCarGoTime, "53" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, 1, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarDistance, "18" }, }, };
            yield return new object[] { (int)ERobotsSymbols.car, 43, new Dictionary<int, string> { { (int)ERobotsSymbols.valCarGoTime, "17" }, { (int)ERobotsSymbols.valCarSpeed, "66" }, { (int)ERobotsSymbols.valCarRotationalSpeed, "94" }, { (int)ERobotsSymbols.valCarTurnTime, "63" }, }, };
        }
        [Theory]
        [MemberData(nameof(Data_SerializeToJsonObject_NotSetTask_ThrowException))]
        public void SerializeToJsonObject_NotSetTask_ThrowException(uint devId, int devType, Dictionary<int, string> extraValues)
        {
            //arrange
            Task taskObj = new Task();
            Dictionary<ERobotsSymbols, string> extraValuesDict = new Dictionary<ERobotsSymbols, string>(); // rewrite extraValues dictionary to change type of key to ERobotsSymbols

            taskObj.devID = devId;
            taskObj.devType = (ERobotsSymbols)devType;

            foreach (KeyValuePair<int, string> extraValue in extraValues)
            {
                taskObj.AddExtraValue((ERobotsSymbols)extraValue.Key, extraValue.Value);
                extraValuesDict.Add((ERobotsSymbols) extraValue.Key, extraValue.Value);
            }

            //prepare extraValues for assert
            string extraValuesResult = string.Join("", extraValuesDict);


            //act & assert
            MsgContainerNotSetException ex = Assert.Throws<MsgContainerNotSetException>(() => taskObj.serializeToJsonObject());

            Assert.Equal("Task container is not correctly set", ex.Message);

            Assert.Equal(devId, ex.Data["devID"]);
            Assert.Equal((ERobotsSymbols)devType, ex.Data["devType"]);
            Assert.Null(ex.Data["task"]);
            Assert.Equal(extraValuesResult, ex.Data["extraValues"]);

        }

        [Fact]
        public void SerializeToJsonObject_NotSetEverything_ThrowException()
        {
            //arrange
            Task taskObj = new Task();

            //act & assert
            MsgContainerNotSetException ex = Assert.Throws<MsgContainerNotSetException>(() => taskObj.serializeToJsonObject());

            Assert.Equal("Task container is not correctly set", ex.Message);

            Assert.Null(ex.Data["devID"]);
            Assert.Null(ex.Data["devType"]);
            Assert.Null(ex.Data["task"]);
            Assert.Equal("", ex.Data["extraValues"]);
            Assert.Null(ex.Data["extraValuesNumber"]);
        }
    }
}
