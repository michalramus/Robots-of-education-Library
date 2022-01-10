using ROELibrary;
using Xunit;
using Moq;
using System;
using System.Collections.Generic;

namespace UnitTests.Robots
{
    public class CarDeviceTests
    {
        [Theory]
        [InlineData(0, new uint[] { 1, 2, 3, 4, 5 }, 6, 7)]
        [InlineData((uint)15, new uint[] { 1, 6, 43, 72, 13 }, (uint)42, (uint)81)]
        [InlineData((uint)40, new uint[] { 3, 2, 4, 8, 32 }, (uint)21, (uint)42)]
        [InlineData((uint)78, new uint[] { 2, 56, 64, 33, 8 }, (uint)36, (uint)64)]
        [InlineData((uint)0, new uint[] { 4, 6, 8, 10, 12 }, (uint)845, (uint)25)]
        [InlineData((uint)18, new uint[] { 15, 66, 21, 30, 44 }, (uint)327, (uint)64)]
        [InlineData((uint)423, new uint[] { 71, 62, 34, 21, 6 }, (uint)43789, (uint)674)]
        [InlineData((uint)44, new uint[] { 23, 5, 7, 47, 49 }, (uint)5, (uint)31)]
        [InlineData((uint)55, new uint[] { 12, 13, 33, 15, 69 }, (uint)845, (uint)676)]
        [InlineData((uint)99, new uint[] { 82, 24, 37, 45, 52 }, (uint)1, (uint)16)]
        [InlineData((uint)1357954, new uint[] { 22, 23, 24, 25, 26 }, (uint)348, (uint)47)]
        [InlineData((uint)21, new uint[] { 31, 61, 42, 32, 11 }, (uint)4239, (uint)423)]
        [InlineData((uint)30, new uint[] { 14, 12, 17, 990, 421 }, (uint)15, (uint)254)]
        [InlineData((uint)76, new uint[] { 6, 100, 111, 222, 333 }, (uint)243, (uint)33)]
        [InlineData((uint)64, new uint[] { 87, 78, 43, 34, 5 }, (uint)660, (uint)8)]
        [InlineData((uint)23, new uint[] { 78, 237, 434, 12, 17 }, (uint)31, (uint)21)]
        [InlineData((uint)80, new uint[] { 30, 2, 15, 4, 8 }, (uint)7, (uint)32)]
        public void CarDevice_setupAndSetConfigMessage_sendMessage(uint id, uint[] pins, uint impulsesPerRotation, uint circumference)
        {
            //arrange
            Mock<CarModel> carModelMock = new Mock<CarModel>();
            carModelMock.Setup(x => x.getModelType()).Returns(ERobotsSymbols.car);
            carModelMock.Setup(x => x.id).Returns(id);
            carModelMock.Setup(x => x.pins).Returns(pins);
            carModelMock.Setup(x => x.impulsesPerRotation).Returns(impulsesPerRotation);
            carModelMock.Setup(x => x.circumference).Returns(circumference);

            Mock<Device> deviceMock = new Mock<Device>().SetupProperty(x => x.pins, new List<uint>());
            Mock<Task> taskMock = new Mock<Task>();
            taskMock.Setup(x => x.AddExtraValue(It.IsAny<ERobotsSymbols>(), It.IsAny<string>()));

            var createMessageContainerMock = new Mock<Func<EMessageSymbols, Func<IMessageContainer>>>();
            createMessageContainerMock.Setup(x => x(EMessageSymbols.msgTypeConfig)).Returns(() => deviceMock.Object);
            createMessageContainerMock.Setup(x => x(EMessageSymbols.msgTypeTask)).Returns(() => taskMock.Object);

            Mock<IMessage> messageMock = new Mock<IMessage>();
            Func<IMessage> messageMockCreator = () => messageMock.Object;

            Mock<Action<IMessage>> sendMessageMock = new Mock<Action<IMessage>>();

            //act
            CarDevice carDevice = new CarDevice(carModelMock.Object, messageMockCreator, createMessageContainerMock.Object, sendMessageMock.Object);

            //assert
            taskMock.VerifySet(x => x.devID = id, Times.Once);
            taskMock.VerifySet(x => x.devType = ERobotsSymbols.car, Times.Once);
            taskMock.VerifySet(x => x.task = ERobotsSymbols.nothingToDo, Times.Once);
            taskMock.Verify(x => x.AddExtraValue(ERobotsSymbols.valCarImpulsesPerRotation, impulsesPerRotation.ToString()), Times.Once);
            taskMock.Verify(x => x.AddExtraValue(ERobotsSymbols.valCarCircumference, circumference.ToString()), Times.Once);

            deviceMock.VerifySet(x => x.devID = id, Times.Once);
            deviceMock.VerifySet(x => x.devType = ERobotsSymbols.car, Times.Once);
            Assert.Equal(deviceMock.Object.pins.ToArray(), pins);

            messageMock.Verify(x => x.addMsgContainer(deviceMock.Object), Times.Once);
            messageMock.Verify(x => x.addMsgContainer(taskMock.Object), Times.Once);

            sendMessageMock.Verify(x => x(messageMock.Object), Times.Exactly(2));

        }

        [Theory]
        [InlineData((uint)15, new uint[] { 1, 2, 3, 4, 5 }, null, (uint)88)]
        [InlineData((uint)0, new uint[] { 1, 2, 3, 4, 5 }, (uint)6, null)]
        [InlineData((uint)15, new uint[] { 1, 6, 43, 72, 13 }, null, (uint)81)]
        [InlineData((uint)40, null, (uint)21, (uint)42)]
        [InlineData(null, new uint[] { 2, 56, 64, 33, 8 }, (uint)36, (uint)64)]
        [InlineData(null, new uint[] { 4, 6, 8, 10, 12 }, null, (uint)25)]
        [InlineData(null, null, (uint)327, (uint)64)]
        [InlineData(null, new uint[] { 71, 62, 34, 21, 6 }, (uint)43789, null)]
        [InlineData((uint)44, null, null, (uint)31)]
        [InlineData((uint)55, null, (uint)845, null)]
        [InlineData((uint)99, new uint[] { 82, 24, 37, 45, 52 }, null, null)]
        [InlineData(null, null, null, (uint)47)]
        [InlineData((uint)21, null, null, null)]
        [InlineData(null, new uint[] { 14, 12, 17, 990, 421 }, null, null)]
        [InlineData(null, null, (uint)243, null)]
        [InlineData(null, null, null, null)]
        [InlineData(null, new uint[] { 78, 237, 434, 12, 17 }, (uint)31, (uint)21)]
        [InlineData((uint)80, new uint[] { 30, 2, 15, 4, 8 }, null, (uint)32)]
        public void CarDevice_passIncorrectlyConfiguredModel_throwException(uint? id, uint[] pins, uint? impulsesPerRotation, uint? circumference)
        {
            //arrange
            Mock<CarModel> carModelMock = new Mock<CarModel>();
            carModelMock.Setup(x => x.getModelType()).Returns(ERobotsSymbols.car);
            carModelMock.Setup(x => x.id).Returns(id);
            carModelMock.Setup(x => x.pins).Returns(pins);
            carModelMock.Setup(x => x.impulsesPerRotation).Returns(impulsesPerRotation);
            carModelMock.Setup(x => x.circumference).Returns(circumference);

            //act & assert
            var ex = Assert.Throws<DeviceModelIncorrectSetupException>(() => new CarDevice(carModelMock.Object, null, null, null));
            Assert.Equal("Some of properties in model are null", ex.Message);
            Assert.Equal(id, ex.Data["id"]);
            Assert.Equal(ERobotsSymbols.car, ex.Data["type"]);
            Assert.Equal(pins, ex.Data["pins"]);
            Assert.Equal(impulsesPerRotation, ex.Data["impulsesPerRotation"]);
            Assert.Equal(circumference, ex.Data["circumference"]);
        }

        [Theory]
        [InlineData((int)ERobotsSymbols.devID)]
        [InlineData((int)ERobotsSymbols.pins)]
        [InlineData((int)ERobotsSymbols.taskCarGo)]
        [InlineData((int)ERobotsSymbols.valCarSpeed)]
        [InlineData((int)ERobotsSymbols.nothingToDo)]
        [InlineData((int)ERobotsSymbols.extraValuesNumber)]
        public void CarDevice_passIncorrectModel_throwException(int devType)
        {
            //arrange
            Mock<CarModel> carModelMock = new Mock<CarModel>();
            carModelMock.Setup(x => x.getModelType()).Returns((ERobotsSymbols)devType);

            //act & assert
            var ex = Assert.Throws<DeviceModelIncorrectSetupException>(() => new CarDevice(carModelMock.Object, null, null, null));
            Assert.Equal("Incorrect DeviceModel: expected CarModel", ex.Message);

            Assert.Equal((ERobotsSymbols)devType, ex.Data["modelType"]);
            Assert.Equal(ERobotsSymbols.car, ex.Data["exceptedModelType"]);
            Assert.Equal(carModelMock.Object.ToString(), ex.Data["model"]);
        }

        [Theory]
        [InlineData(new uint[] { 22, 23, 24, 31, 61, 42, 32, 11, 14, 12, 17, 990, 421 })]
        [InlineData(new uint[] { 6, 100, 111, 222, 333, 87, 78, 43, 34, 5 })]
        [InlineData(new uint[] { 78, 237, 434, 12, 17, 30, 2, 15, 4 })]
        [InlineData(new uint[] { 1, 6, 43, 72, 13, 3, 2, 4, })]
        [InlineData(new uint[] { 4, 6, 12, 15, 66, 21, 30 })]
        [InlineData(new uint[] { 12, 13, 33, 15, 69, 82 })]
        [InlineData(new uint[] { 23, 5, 7, 47 })]
        [InlineData(new uint[] { 1, 4, 5 })]
        [InlineData(new uint[] { 2, 56, })]
        [InlineData(new uint[] { 71 })]
        [InlineData(new uint[] { })]
        public void CarDevice_passPinArrayWithIncorrectSize_throwException(uint[] pins)
        {
            //arrange
            uint id = 16;
            uint impulsesPerRotation = 87;
            uint circumference = 42;

            Mock<CarModel> carModelMock = new Mock<CarModel>();
            carModelMock.Setup(x => x.getModelType()).Returns(ERobotsSymbols.car);
            carModelMock.Setup(x => x.id).Returns(id);
            carModelMock.Setup(x => x.pins).Returns(pins);
            carModelMock.Setup(x => x.impulsesPerRotation).Returns(impulsesPerRotation);
            carModelMock.Setup(x => x.circumference).Returns(circumference);

            //act & assert
            var ex = Assert.Throws<DeviceModelIncorrectSetupException>(() => new CarDevice(carModelMock.Object, null, null, null)); //carDevice should check input data before creating and sending message(thats why we don't need to mock delegates)
            Assert.Equal("Car device model must have defined 5 pins", ex.Message);
            Assert.Equal(id, ex.Data["id"]);
            Assert.Equal(ERobotsSymbols.car, ex.Data["type"]);
            Assert.Equal(pins, ex.Data["pins"]);
            Assert.Equal(impulsesPerRotation, ex.Data["impulsesPerRotation"]);
            Assert.Equal(circumference, ex.Data["circumference"]);
        }



        //methods

        [Theory]
        [InlineData((uint)15, (uint)42)]
        [InlineData((uint)40, (uint)21)]
        [InlineData((uint)78, (uint)36)]
        [InlineData((uint)0, (uint)845)]
        [InlineData((uint)18, (uint)327)]
        [InlineData((uint)423, (uint)43789)]
        [InlineData((uint)44, (uint)4213750)]
        [InlineData((uint)55, (uint)845)]
        [InlineData((uint)99, (uint)4597)]
        [InlineData((uint)1357954, (uint)348)]
        [InlineData((uint)21, (uint)4239)]
        [InlineData((uint)30, (uint)3000)]
        [InlineData((uint)76, (uint)243)]
        [InlineData((uint)64, (uint)660)]
        [InlineData((uint)23, (uint)3238)]
        [InlineData((uint)80, (uint)7)]
        public void goForward_sendTaskMessage_sendMessage(uint? id, uint distance)
        {
            //arrange
            uint[] pins = new uint[] { 1, 2, 3, 4, 5 };
            uint impulsesPerRotation = 31;
            uint circumference = 88;

            ERobotsSymbols devType = ERobotsSymbols.car;
            ERobotsSymbols task = ERobotsSymbols.taskCarGo;

            Mock<CarModel> carModelMock = new Mock<CarModel>();
            carModelMock.Setup(x => x.getModelType()).Returns(devType);
            carModelMock.Setup(x => x.id).Returns(id);
            carModelMock.Setup(x => x.pins).Returns(pins);
            carModelMock.Setup(x => x.impulsesPerRotation).Returns(impulsesPerRotation);
            carModelMock.Setup(x => x.circumference).Returns(circumference);

            Mock<Device> deviceMock = new Mock<Device>().SetupProperty(x => x.pins, new List<uint>());
            Mock<Task> taskMock = new Mock<Task>();
            taskMock.Setup(x => x.AddExtraValue(It.IsAny<ERobotsSymbols>(), It.IsAny<string>()));

            var createMessageContainerMock = new Mock<Func<EMessageSymbols, Func<IMessageContainer>>>();
            createMessageContainerMock.Setup(x => x(EMessageSymbols.msgTypeConfig)).Returns(() => deviceMock.Object);
            createMessageContainerMock.Setup(x => x(EMessageSymbols.msgTypeTask)).Returns(() => taskMock.Object);

            Mock<IMessage> messageMock = new Mock<IMessage>();
            Func<IMessage> messageMockCreator = () => messageMock.Object;

            Mock<Action<IMessage>> sendMessageMock = new Mock<Action<IMessage>>();

            //act
            CarDevice carDevice = new CarDevice(carModelMock.Object, messageMockCreator, createMessageContainerMock.Object, sendMessageMock.Object);
            carDevice.goForward(distance);

            //assert
            taskMock.VerifySet(x => x.devID = id, Times.Exactly(2));
            taskMock.VerifySet(x => x.devType = devType, Times.Exactly(2));
            taskMock.VerifySet(x => x.task = task, Times.Once);
            taskMock.Verify(x => x.AddExtraValue(ERobotsSymbols.valCarDistance, distance.ToString()), Times.Once);

            messageMock.Verify(x => x.addMsgContainer(taskMock.Object), Times.Exactly(2)); //first to send extraValues, second to send task

            sendMessageMock.Verify(x => x(messageMock.Object), Times.Exactly(3)); //first to send config message, second to send extraValues, third to send task

        }

        [Theory]
        [InlineData((uint)33, (uint)84)]
        [InlineData((uint)15, (uint)42)]
        [InlineData((uint)40, (uint)21)]
        [InlineData((uint)78, (uint)36)]
        [InlineData((uint)0, (uint)845)]
        [InlineData((uint)18, (uint)327)]
        [InlineData((uint)423, (uint)43789)]
        [InlineData((uint)44, (uint)4213750)]
        [InlineData((uint)55, (uint)845)]
        [InlineData((uint)99, (uint)4597)]
        [InlineData((uint)1357954, (uint)348)]
        [InlineData((uint)21, (uint)4239)]
        [InlineData((uint)30, (uint)3000)]
        [InlineData((uint)76, (uint)243)]
        [InlineData((uint)64, (uint)660)]
        [InlineData((uint)23, (uint)3238)]
        [InlineData((uint)80, (uint)7)]
        public void rotate_sendTaskMessage_sendMessage(uint? id, uint angle)
        {
            //arrange
            uint[] pins = new uint[] { 1, 2, 3, 4, 5 };
            uint impulsesPerRotation = 31;
            uint circumference = 88;

            ERobotsSymbols devType = ERobotsSymbols.car;
            ERobotsSymbols task = ERobotsSymbols.taskCarTurn;

            Mock<CarModel> carModelMock = new Mock<CarModel>();
            carModelMock.Setup(x => x.getModelType()).Returns(devType);
            carModelMock.Setup(x => x.id).Returns(id);
            carModelMock.Setup(x => x.pins).Returns(pins);
            carModelMock.Setup(x => x.impulsesPerRotation).Returns(impulsesPerRotation);
            carModelMock.Setup(x => x.circumference).Returns(circumference);

            Mock<Device> deviceMock = new Mock<Device>().SetupProperty(x => x.pins, new List<uint>());
            Mock<Task> taskMock = new Mock<Task>();
            taskMock.Setup(x => x.AddExtraValue(It.IsAny<ERobotsSymbols>(), It.IsAny<string>()));

            var createMessageContainerMock = new Mock<Func<EMessageSymbols, Func<IMessageContainer>>>();
            createMessageContainerMock.Setup(x => x(EMessageSymbols.msgTypeConfig)).Returns(() => deviceMock.Object);
            createMessageContainerMock.Setup(x => x(EMessageSymbols.msgTypeTask)).Returns(() => taskMock.Object);

            Mock<IMessage> messageMock = new Mock<IMessage>();
            Func<IMessage> messageMockCreator = () => messageMock.Object;

            Mock<Action<IMessage>> sendMessageMock = new Mock<Action<IMessage>>();

            //act
            CarDevice carDevice = new CarDevice(carModelMock.Object, messageMockCreator, createMessageContainerMock.Object, sendMessageMock.Object);
            carDevice.rotate(angle);

            //assert
            taskMock.VerifySet(x => x.devID = id, Times.Exactly(2));
            taskMock.VerifySet(x => x.devType = devType, Times.Exactly(2));
            taskMock.VerifySet(x => x.task = task, Times.Once);
            taskMock.Verify(x => x.AddExtraValue(ERobotsSymbols.valCarAngle, angle.ToString()), Times.Once);

            messageMock.Verify(x => x.addMsgContainer(taskMock.Object), Times.Exactly(2)); //first to send extraValues, second to send task

            sendMessageMock.Verify(x => x(messageMock.Object), Times.Exactly(3)); //first to send config message, second to send extraValues, third to send task
        }

        [Theory]
        [InlineData((uint)50, (uint)44)]
        [InlineData((uint)15, (uint)42)]
        [InlineData((uint)40, (uint)21)]
        [InlineData((uint)78, (uint)36)]
        [InlineData((uint)0, (uint)845)]
        [InlineData((uint)18, (uint)327)]
        [InlineData((uint)423, (uint)43789)]
        [InlineData((uint)44, (uint)4213750)]
        [InlineData((uint)55, (uint)845)]
        [InlineData((uint)99, (uint)4597)]
        [InlineData((uint)1357954, (uint)348)]
        [InlineData((uint)21, (uint)4239)]
        [InlineData((uint)30, (uint)3000)]
        [InlineData((uint)76, (uint)243)]
        [InlineData((uint)64, (uint)660)]
        [InlineData((uint)23, (uint)3238)]
        [InlineData((uint)80, (uint)7)]
        public void setSpeed_sendTaskMessage_sendMessage(uint? id, uint speed)
        {
            //arrange
            uint[] pins = new uint[] { 1, 2, 3, 4, 5 };
            uint impulsesPerRotation = 31;
            uint circumference = 88;

            ERobotsSymbols devType = ERobotsSymbols.car;
            ERobotsSymbols task = ERobotsSymbols.nothingToDo;

            Mock<CarModel> carModelMock = new Mock<CarModel>();
            carModelMock.Setup(x => x.getModelType()).Returns(devType);
            carModelMock.Setup(x => x.id).Returns(id);
            carModelMock.Setup(x => x.pins).Returns(pins);
            carModelMock.Setup(x => x.impulsesPerRotation).Returns(impulsesPerRotation);
            carModelMock.Setup(x => x.circumference).Returns(circumference);

            Mock<Device> deviceMock = new Mock<Device>().SetupProperty(x => x.pins, new List<uint>());
            Mock<Task> taskMock = new Mock<Task>();
            taskMock.Setup(x => x.AddExtraValue(It.IsAny<ERobotsSymbols>(), It.IsAny<string>()));

            var createMessageContainerMock = new Mock<Func<EMessageSymbols, Func<IMessageContainer>>>();
            createMessageContainerMock.Setup(x => x(EMessageSymbols.msgTypeConfig)).Returns(() => deviceMock.Object);
            createMessageContainerMock.Setup(x => x(EMessageSymbols.msgTypeTask)).Returns(() => taskMock.Object);

            Mock<IMessage> messageMock = new Mock<IMessage>();
            Func<IMessage> messageMockCreator = () => messageMock.Object;

            Mock<Action<IMessage>> sendMessageMock = new Mock<Action<IMessage>>();

            //act
            CarDevice carDevice = new CarDevice(carModelMock.Object, messageMockCreator, createMessageContainerMock.Object, sendMessageMock.Object);
            carDevice.setSpeed(speed);

            //assert
            taskMock.VerifySet(x => x.devID = id, Times.Exactly(2));
            taskMock.VerifySet(x => x.devType = devType, Times.Exactly(2));
            taskMock.VerifySet(x => x.task = task, Times.Exactly(2)); //config message send nothingToDo task too
            taskMock.Verify(x => x.AddExtraValue(ERobotsSymbols.valCarSpeed, speed.ToString()), Times.Once);

            messageMock.Verify(x => x.addMsgContainer(taskMock.Object), Times.Exactly(2)); //first to send extraValues, second to send task

            sendMessageMock.Verify(x => x(messageMock.Object), Times.Exactly(3)); //first to send config message, second to send extraValues, third to send task
        }

        [Theory]
        [InlineData((uint)15, (uint)42)]
        [InlineData((uint)40, (uint)21)]
        [InlineData((uint)78, (uint)36)]
        [InlineData((uint)0, (uint)845)]
        [InlineData((uint)18, (uint)327)]
        [InlineData((uint)423, (uint)43789)]
        [InlineData((uint)44, (uint)4213750)]
        [InlineData((uint)55, (uint)845)]
        [InlineData((uint)99, (uint)4597)]
        [InlineData((uint)1357954, (uint)348)]
        [InlineData((uint)21, (uint)4239)]
        [InlineData((uint)30, (uint)3000)]
        [InlineData((uint)76, (uint)243)]
        [InlineData((uint)64, (uint)660)]
        [InlineData((uint)23, (uint)3238)]
        [InlineData((uint)80, (uint)7)]
        public void setRotationalSpeed_sendTaskMessage_sendMessage(uint? id, uint rotationalSpeed)
        {
            //arrange
            uint[] pins = new uint[] { 1, 2, 3, 4, 5 };
            uint impulsesPerRotation = 31;
            uint circumference = 88;

            ERobotsSymbols devType = ERobotsSymbols.car;
            ERobotsSymbols task = ERobotsSymbols.nothingToDo;

            Mock<CarModel> carModelMock = new Mock<CarModel>();
            carModelMock.Setup(x => x.getModelType()).Returns(devType);
            carModelMock.Setup(x => x.id).Returns(id);
            carModelMock.Setup(x => x.pins).Returns(pins);
            carModelMock.Setup(x => x.impulsesPerRotation).Returns(impulsesPerRotation);
            carModelMock.Setup(x => x.circumference).Returns(circumference);

            Mock<Device> deviceMock = new Mock<Device>().SetupProperty(x => x.pins, new List<uint>());
            Mock<Task> taskMock = new Mock<Task>();
            taskMock.Setup(x => x.AddExtraValue(It.IsAny<ERobotsSymbols>(), It.IsAny<string>()));

            var createMessageContainerMock = new Mock<Func<EMessageSymbols, Func<IMessageContainer>>>();
            createMessageContainerMock.Setup(x => x(EMessageSymbols.msgTypeConfig)).Returns(() => deviceMock.Object);
            createMessageContainerMock.Setup(x => x(EMessageSymbols.msgTypeTask)).Returns(() => taskMock.Object);

            Mock<IMessage> messageMock = new Mock<IMessage>();
            Func<IMessage> messageMockCreator = () => messageMock.Object;

            Mock<Action<IMessage>> sendMessageMock = new Mock<Action<IMessage>>();

            //act
            CarDevice carDevice = new CarDevice(carModelMock.Object, messageMockCreator, createMessageContainerMock.Object, sendMessageMock.Object);
            carDevice.setSpeed(rotationalSpeed);

            //assert
            taskMock.VerifySet(x => x.devID = id, Times.Exactly(2));
            taskMock.VerifySet(x => x.devType = devType, Times.Exactly(2));
            taskMock.VerifySet(x => x.task = task, Times.Exactly(2)); //config message send nothingToDo task too
            taskMock.Verify(x => x.AddExtraValue(ERobotsSymbols.valCarSpeed, rotationalSpeed.ToString()), Times.Once);

            messageMock.Verify(x => x.addMsgContainer(taskMock.Object), Times.Exactly(2)); //first to send extraValues, second to send task

            sendMessageMock.Verify(x => x(messageMock.Object), Times.Exactly(3)); //first to send config message, second to send extraValues, third to send task
        }
    }
}
