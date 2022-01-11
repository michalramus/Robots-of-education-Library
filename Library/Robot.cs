using System.Collections.Generic;
using System.IO.Ports;
using System;

namespace ROELibrary
{
    public class Robot
    {
        //dictionaries of devices
        public Dictionary<int, ICarDevice> cars { get; private set; } = new Dictionary<int, ICarDevice>();

        //communication
        SerialPort _serialPort;
        ISerialPort _serialPortFacade;
        ICommunication communication;
        public Robot(Configure libraryConfig, VRobotModel[] robotsModels)
        {
            if (libraryConfig.isModelSetup() == false)
            {
                //TODO: log data
                //TODO: throw exception
            }

            //setup serial port
            _serialPort = new SerialPort();
            _serialPort.PortName = libraryConfig.serialPortName;
            _serialPort.BaudRate = (int)libraryConfig.baudRate;
            _serialPort.Open();

            _serialPortFacade = new SerialPortFacade(_serialPort);
            communication = new SerialCommunication(_serialPortFacade);


            //setup devices
            foreach (VRobotModel model in robotsModels)
            {
                switch (model.getModelType())
                {
                    case ERobotsSymbols.car:
                        addCar(model);
                        break;
                }
            }
        }

        private void addCar(VRobotModel model)
        {
            try
            {
                IRobotDevice car = RobotsFactory.getDeviceCreator(ERobotsSymbols.car)(model, sendMessage); //create car
                cars.Add((int)model.getID(), (ICarDevice)car);
            }
            catch (Exception ex)
            {
                //TODO: log data
                throw;
            }
        }

       private void sendMessage(IMessage msg)
       {
           //TODO:
       }
    }
}

