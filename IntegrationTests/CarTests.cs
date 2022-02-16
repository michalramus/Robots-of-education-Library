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
            serialPortMock.Setup(x => x.ReadLine()).Returns("x{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}x");


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

            serialPortMock.Verify(x => x.Write("x{\"type\":\"conf\",\"dev\":[{\"devType\":\"car\",\"ID\":1,\"pins\":[1,2,3,4,5]}]}x"), Times.Once);
            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"nothing\",\"exValNum\":2,\"exVal\":[[\"vCarImpPerRot\",\"70\"],[\"vCarCircumf\",\"100\"]]}]}x"), Times.Once);
        }

        [Fact]
        public void CarDeviceGoForward()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("x{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}x");


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

            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"250\"],[\"vCarDirect\",\"forw\"]]}]}x"), Times.Once);
        }


        [Fact]
        public void CarDeviceGoBackward()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("x{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}x");


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

            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"143\"],[\"vCarDirect\",\"back\"]]}]}x"), Times.Once);
        }

        [Fact]
        public void CarDeviceRotate()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("x{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}x");


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

            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarAngle\",\"1430\"],[\"vCarDirect\",\"left\"]]}]}x"), Times.Once);
            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarAngle\",\"324\"],[\"vCarDirect\",\"right\"]]}]}x"), Times.Once);
        }


        [Fact]
        public void CarDeviceSetSpeed()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("x{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}x");


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

            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"nothing\",\"exValNum\":1,\"exVal\":[[\"vCarSpeed\",\"75\"]]}]}x"), Times.Once);
        }


        [Fact]
        public void CarDeviceSetRotationalSpeed()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("x{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}x");


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

            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":1,\"task\":\"nothing\",\"exValNum\":1,\"exVal\":[[\"vCarRotSpeed\",\"21\"]]}]}x"), Times.Once);
        }


        [Fact]
        public void CarDeviceCallMultiActions()
        {
            Mock<ISerialPort> serialPortMock = new Mock<ISerialPort>();
            serialPortMock.Setup(x => x.Write(It.IsAny<string>()));
            serialPortMock.Setup(x => x.IsOpen()).Returns(true);
            serialPortMock.Setup(x => x.BytesToRead()).Returns(2); //more than 1
            serialPortMock.Setup(x => x.ReadLine()).Returns("x{\"type\":\"info\",\"data\":[[\"msgReceived\", \"true\"]]}x");


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
            robot.cars[3].setSpeed(44);
            robot.cars[3].setRotationalSpeed(37);
            robot.cars[3].goForward(463);
            robot.cars[3].goBackward(113);
            robot.cars[3].rotate(40, true);
            robot.cars[3].rotate(60, false);

            serialPortMock.Verify(x => x.Write("x{\"type\":\"conf\",\"dev\":[{\"devType\":\"car\",\"ID\":3,\"pins\":[1,25,13,47,50]}]}x"), Times.Once);
            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"nothing\",\"exValNum\":2,\"exVal\":[[\"vCarImpPerRot\",\"349\"],[\"vCarCircumf\",\"68\"]]}]}x"), Times.Once);

            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"nothing\",\"exValNum\":1,\"exVal\":[[\"vCarSpeed\",\"44\"]]}]}x"), Times.Once);
            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"nothing\",\"exValNum\":1,\"exVal\":[[\"vCarRotSpeed\",\"37\"]]}]}x"), Times.Once);
            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"463\"],[\"vCarDirect\",\"forw\"]]}]}x"), Times.Once);
            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"tCarGo\",\"exValNum\":2,\"exVal\":[[\"vCarDist\",\"113\"],[\"vCarDirect\",\"back\"]]}]}x"), Times.Once);
            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarAngle\",\"40\"],[\"vCarDirect\",\"right\"]]}]}x"), Times.Once);
            serialPortMock.Verify(x => x.Write("x{\"type\":\"task\",\"tasks\":[{\"devType\":\"car\",\"ID\":3,\"task\":\"tCarTurn\",\"exValNum\":2,\"exVal\":[[\"vCarAngle\",\"60\"],[\"vCarDirect\",\"left\"]]}]}x"), Times.Once);
        }

        //TODO: 2 the same id's; test rotate method; incorrect car model setup; check errorMsg
    }
}
