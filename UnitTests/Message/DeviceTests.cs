using Xunit;
using ROELibrary;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace UnitTests.Msg
{
    public class DeviceTests
    {
        //TODO: change test
        [Theory]
        [InlineData(0, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":0,\"pins\":[1,15,7,42,6]}", 1, 15, 7, 42, 6)]
        [InlineData(15, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":15,\"pins\":[18,1,30]}", 18, 1, 30)]
        [InlineData(7843, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":7843,\"pins\":[14,15]}", 14, 15)]
        [InlineData(1008, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":1008,\"pins\":[1,15,6,7,8,9,10]}", 1, 15, 6, 7, 8, 9, 10)]
        [InlineData(78776, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":78776,\"pins\":[1,2,3,4,5]}", 1, 2, 3, 4, 5)]
        [InlineData(4341, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":4341,\"pins\":[7,14,85,61]}", 7, 14, 85, 61)]
        [InlineData(89, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":89,\"pins\":[1,3,2,4,5]}", 1, 3, 2, 4, 5)]
        [InlineData(2, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":2,\"pins\":[6,16,44,42,3]}", 6, 16, 44, 42, 3)]
        [InlineData(10, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":10,\"pins\":[8,44,52,63,70]}", 8, 44, 52, 63, 70)]
        [InlineData(71, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":71,\"pins\":[44,15,6,1,2]}", 44, 15, 6, 1, 2)]
        [InlineData(898076, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":898076,\"pins\":[7,18,17,16,15]}", 7, 18, 17, 16, 15)]
        [InlineData(554, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":554,\"pins\":[4,2,1,6]}", 4, 2, 1, 6)]
        [InlineData(233, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":233,\"pins\":[5]}", 5)]
        [InlineData(9086, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":9086,\"pins\":[88,45,32]}", 88, 45, 32)]
        [InlineData(33, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":33,\"pins\":[3,20,31,42]}", 3, 20, 31, 42)]
        [InlineData(0, (int)ERobotsSymbols.car, "{\"devType\":\"car\",\"ID\":0,\"pins\":[7,77,14,91,22,37,14]}", 7, 77, 14, 91, 22, 37, 14)]
        public void SerializeToJsonObject_SerializeDevice_ReturnsCorrectJson(uint devId, int devType, string json, params int[] pins)
        {
            //Arrange
            Device device = new Device();

            device.devID = devId;
            device.devType = (ERobotsSymbols)devType;
            foreach (int pin in pins)
            {
                device.pins.Add((uint)pin);
            }

            //Act
            JObject jsonObj = device.serializeToJsonObject();
            string result = jsonObj.ToString();

            result = result.Replace("\n", "");
            result = result.Replace(" ", "");
            result = result.Replace("\r", "");

            //Assert
            Assert.Equal(json, result);
        }

        [Theory]
        [InlineData((int)ERobotsSymbols.car, 1, 15, 7, 42, 6)]
        [InlineData((int)ERobotsSymbols.car, 18, 1, 30)]
        [InlineData((int)ERobotsSymbols.car, 14, 15)]
        [InlineData((int)ERobotsSymbols.car, 1, 15, 6, 7, 8, 9, 10)]
        [InlineData((int)ERobotsSymbols.car, 1, 2, 3, 4, 5)]
        [InlineData((int)ERobotsSymbols.car, 7, 14, 85, 61)]
        [InlineData((int)ERobotsSymbols.car, 1, 3, 2, 4, 5)]
        [InlineData((int)ERobotsSymbols.car, 6, 16, 44, 42, 3)]
        [InlineData((int)ERobotsSymbols.car, 8, 44, 52, 63, 70)]
        [InlineData((int)ERobotsSymbols.car, 44, 15, 6, 1, 2)]
        [InlineData((int)ERobotsSymbols.car, 7, 18, 17, 16, 15)]
        [InlineData((int)ERobotsSymbols.car, 4, 2, 1, 6)]
        [InlineData((int)ERobotsSymbols.car, 5)]
        [InlineData((int)ERobotsSymbols.car, 88, 45, 32)]
        [InlineData((int)ERobotsSymbols.car, 3, 20, 31, 42)]
        [InlineData((int)ERobotsSymbols.car, 7, 77, 14, 91, 22, 37, 14)]
        public void SerializeToJsonObject_NotSetDeviceID_ThrowException(int devType, params int[] pins)
        {
            //Arrange
            List<uint> pinsList = new List<uint>(); //rewrite pins list to uint type, because can't project to uint

            Device device = new Device();
            device.devType = (ERobotsSymbols)devType;
            foreach (int pin in pins)
            {
                device.pins.Add((uint)pin);
                pinsList.Add((uint)pin);
            }

            //Act & Assert
            MsgContainerNotSetException ex = Assert.Throws<MsgContainerNotSetException>(() => device.serializeToJsonObject());

            Assert.Equal("Device container is not correctly set", ex.Message);

            Assert.Null(ex.Data["devID"]);
            Assert.Equal((ERobotsSymbols)devType, ex.Data["devType"]);
            Assert.Equal(pinsList, ex.Data["pins"]);

        }

        [Theory]
        [InlineData(0, 1, 15, 7, 42, 6)]
        [InlineData(15, 18, 1, 30)]
        [InlineData(7843, 14, 15)]
        [InlineData(1008, 1, 15, 6, 7, 8, 9, 10)]
        [InlineData(78776, 1, 2, 3, 4, 5)]
        [InlineData(4341, 7, 14, 85, 61)]
        [InlineData(89, 1, 3, 2, 4, 5)]
        [InlineData(2, 6, 16, 44, 42, 3)]
        [InlineData(10, 8, 44, 52, 63, 70)]
        [InlineData(71, 44, 15, 6, 1, 2)]
        [InlineData(898076, 7, 18, 17, 16, 15)]
        [InlineData(554, 4, 2, 1, 6)]
        [InlineData(233, 5)]
        [InlineData(9086, 88, 45, 32)]
        [InlineData(33, 3, 20, 31, 42)]
        [InlineData(0, 7, 77, 14, 91, 22, 37, 14)]
        public void SerializeToJsonObject_NotSetDeviceType_ThrowException(uint devId, params int[] pins)
        {
            //Arrange
            List<uint> pinsList = new List<uint>(); //rewrite pins list to uint type, because can't project to uint

            Device device = new Device();
            device.devID = devId;
            foreach (int pin in pins)
            {
                device.pins.Add((uint)pin);
                pinsList.Add((uint)pin);
            }

            //Act & Assert
            MsgContainerNotSetException ex = Assert.Throws<MsgContainerNotSetException>(() => device.serializeToJsonObject());

            Assert.Equal("Device container is not correctly set", ex.Message);

            Assert.Equal(devId, ex.Data["devID"]);
            Assert.Null(ex.Data["devType"]);
            Assert.Equal(pinsList, ex.Data["pins"]);

        }

        [Theory]
        [InlineData(15, (int)ERobotsSymbols.car)]
        [InlineData(7843, (int)ERobotsSymbols.car)]
        [InlineData(1008, (int)ERobotsSymbols.car)]
        [InlineData(78776, (int)ERobotsSymbols.car)]
        [InlineData(4341, (int)ERobotsSymbols.car)]
        [InlineData(89, (int)ERobotsSymbols.car)]
        [InlineData(2, (int)ERobotsSymbols.car)]
        [InlineData(10, (int)ERobotsSymbols.car)]
        [InlineData(71, (int)ERobotsSymbols.car)]
        [InlineData(898076, (int)ERobotsSymbols.car)]
        [InlineData(554, (int)ERobotsSymbols.car)]
        [InlineData(233, (int)ERobotsSymbols.car)]
        [InlineData(9086, (int)ERobotsSymbols.car)]
        [InlineData(33, (int)ERobotsSymbols.car)]
        [InlineData(0, (int)ERobotsSymbols.car)]
        public void SerializeToJsonObject_NotSetPins_ThrowException(uint devId, int devType)
        {
            //Arrange
            Device device = new Device();
            device.devID = devId;
            device.devType = (ERobotsSymbols)devType;

            //Act & Assert
            MsgContainerNotSetException ex = Assert.Throws<MsgContainerNotSetException>(() => device.serializeToJsonObject());

            Assert.Equal("Device container is not correctly set", ex.Message);

            Assert.Equal(devId, ex.Data["devID"]);
            Assert.Equal((ERobotsSymbols)devType, ex.Data["devType"]);
            Assert.Equal(new uint[0], ex.Data["pins"]);


        }

        [Fact]
        public void SerializeToJsonObject_NotSetEverythink_ThrowException()
        {
            //Arrange
            Device device = new Device();

            //Act & Assert
            MsgContainerNotSetException ex = Assert.Throws<MsgContainerNotSetException>(() => device.serializeToJsonObject());

            Assert.Equal("Device container is not correctly set", ex.Message);

            Assert.Null(ex.Data["devID"]);
            Assert.Null(ex.Data["devType"]);
            Assert.Equal(new int[0], ex.Data["pins"]);
        }
    }
}
