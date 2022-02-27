using Moq;
using Xunit;
using ROELibrary;

namespace IntegrationTests.Robots
{
    public class CarDevice
    {
        [Fact]
        public void setupCarDevice()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("~{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}~");


            Configure configure = new Configure()
            {
                serialPortName = "COM3",
                serialPort = serialPortMock.Object,
            };


            CarModel carModel = new CarModel()
            {
                id = 1,
                pins = new uint[] { 1, 2, 3, 4, 5 },
                impulsesPerRotation = 70,
                circumference = 100,
            };

            Robot robot = new Robot(configure, new CarModel[] { carModel });

            serialPortMock.Verify(x => x.Write("~{\"type\":\"conf\",\"dev\":[{\"devType\":\"car\",\"ID\":1,\"pins\":[1,2,3,4,5]}]}~"), Times.Once);
            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"nothing\",\"exValNum\":2,\"exVal\":[[\"vCarImpPerRot\",\"70\"],[\"vCarCircumf\",\"100\"]]}]}~"), Times.Once);
        }

        [Fact]
        public void CarDeviceGoForward()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("~{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}~");


            Configure configure = new Configure()
            {
                serialPortName = "COM3",
                serialPort = serialPortMock.Object,
            };


            CarModel carModel = new CarModel()
            {
                id = 1,
                pins = new uint[] { 1, 2, 3, 4, 5 },
                impulsesPerRotation = 70,
                circumference = 100,
            };

            Robot robot = new Robot(configure, new CarModel[] { carModel });
            robot.cars[1].goForward(250);

            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"250\"],[\"vCarDirect\",\"forw\"]]}]}~"), Times.Once);
        }


        [Fact]
        public void CarDeviceGoBackward()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("~{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}~");


            Configure configure = new Configure()
            {
                serialPortName = "COM3",
                serialPort = serialPortMock.Object,
            };


            CarModel carModel = new CarModel()
            {
                id = 1,
                pins = new uint[] { 1, 2, 3, 4, 5 },
                impulsesPerRotation = 70,
                circumference = 100,
            };

            Robot robot = new Robot(configure, new CarModel[] { carModel });
            robot.cars[1].goBackward(143);

            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"143\"],[\"vCarDirect\",\"back\"]]}]}~"), Times.Once);
        }

        [Fact]
        public void CarDeviceRotate()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("~{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}~");


            Configure configure = new Configure()
            {
                serialPortName = "COM3",
                serialPort = serialPortMock.Object,
            };


            CarModel carModel = new CarModel()
            {
                id = 1,
                pins = new uint[] { 1, 2, 3, 4, 5 },
                impulsesPerRotation = 70,
                circumference = 100,
            };

            Robot robot = new Robot(configure, new CarModel[] { carModel });
            robot.cars[1].rotate(1430, false);
            robot.cars[1].rotate(324, true);

            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarAngle\",\"1430\"],[\"vCarDirect\",\"left\"]]}]}~"), Times.Once);
            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarAngle\",\"324\"],[\"vCarDirect\",\"right\"]]}]}~"), Times.Once);
        }


        [Fact]
        public void CarDeviceSetSpeed()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("~{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}~");


            Configure configure = new Configure()
            {
                serialPortName = "COM3",
                serialPort = serialPortMock.Object,
            };


            CarModel carModel = new CarModel()
            {
                id = 1,
                pins = new uint[] { 1, 2, 3, 4, 5 },
                impulsesPerRotation = 70,
                circumference = 100,
            };

            Robot robot = new Robot(configure, new CarModel[] { carModel });
            robot.cars[1].setSpeed(75);

            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"nothing\",\"exValNum\":1,\"exVal\":[[\"vCarSpeed\",\"75\"]]}]}~"), Times.Once);
        }


        [Fact]
        public void CarDeviceSetRotationalSpeed()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("~{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}~");


            Configure configure = new Configure()
            {
                serialPortName = "COM3",
                serialPort = serialPortMock.Object,
            };


            CarModel carModel = new CarModel()
            {
                id = 1,
                pins = new uint[] { 1, 2, 3, 4, 5 },
                impulsesPerRotation = 70,
                circumference = 100,
            };

            Robot robot = new Robot(configure, new CarModel[] { carModel });
            robot.cars[1].setRotationalSpeed(21);

            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"nothing\",\"exValNum\":1,\"exVal\":[[\"vCarRotSpeed\",\"21\"]]}]}~"), Times.Once);
        }


        [Fact]
        public void CarDeviceCallMultiActions()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("~{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}~");


            Configure configure = new Configure()
            {
                serialPortName = "COM3",
                serialPort = serialPortMock.Object,
            };


            CarModel carModel = new CarModel()
            {
                id = 3,
                pins = new uint[] { 1, 25, 13, 47, 50 },
                impulsesPerRotation = 349,
                circumference = 68,
            };

            Robot robot = new Robot(configure, new CarModel[] { carModel });

            serialPortMock.Verify(x => x.Write("~{\"type\":\"conf\",\"dev\":[{\"devType\":\"car\",\"ID\":3,\"pins\":[1,25,13,47,50]}]}~"), Times.Once);
            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"nothing\",\"exValNum\":2,\"exVal\":[[\"vCarImpPerRot\",\"349\"],[\"vCarCircumf\",\"68\"]]}]}~"), Times.Once);

            robot.cars[3].setSpeed(44);
            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"nothing\",\"exValNum\":1,\"exVal\":[[\"vCarSpeed\",\"44\"]]}]}~"), Times.Once);

            robot.cars[3].setRotationalSpeed(37);
            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"nothing\",\"exValNum\":1,\"exVal\":[[\"vCarRotSpeed\",\"37\"]]}]}~"), Times.Once);

            robot.cars[3].goForward(463);
            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"463\"],[\"vCarDirect\",\"forw\"]]}]}~"), Times.Once);

            robot.cars[3].goBackward(113);
            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"113\"],[\"vCarDirect\",\"back\"]]}]}~"), Times.Once);

            robot.cars[3].rotate(40, true);
            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarAngle\",\"40\"],[\"vCarDirect\",\"right\"]]}]}~"), Times.Once);

            robot.cars[3].rotate(60, false);
            serialPortMock.Verify(x => x.Write("~{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarAngle\",\"60\"],[\"vCarDirect\",\"left\"]]}]}~"), Times.Once);
        }

        //TODO: 2 the same id's; test rotate method; incorrect car model setup; check errorMsg
    }
}
