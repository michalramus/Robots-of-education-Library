using System.Collections.Generic;
using System.IO.Ports;
using System;
using Serilog;

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

        //logger

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
            _serialPort.ReadBufferSize = (int)libraryConfig.readBufferSize;
            _serialPort.WriteBufferSize = (int)libraryConfig.writeBufferSize;
            _serialPort.ReadTimeout = (int)libraryConfig.readTimeout;
            _serialPort.Open();

            if (libraryConfig.serialPort != null) //use custom serial port
            {
                _serialPortFacade = libraryConfig.serialPort;
            }
            else
            {
            _serialPortFacade = new SerialPortFacade(_serialPort);
            }

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
                if (cars.ContainsKey((int)model.getID()))
                {
                    var ex = new DeviceModelIncorrectSetupException("Id of car is not unique");
                    ex.Data["modelType"] = model.getModelType();
                    ex.Data["exceptedModelType"] = ERobotsSymbols.car;
                    ex.Data["model"] = model.ToString();
                    throw ex;
                }

                cars.Add((int)model.getID(), (ICarDevice)car);
            }
            catch (Exception ex)
            {
                //TODO: log data
                throw;
            }
        }

        /// <summary>
        /// send message and check response. If response is not correct, throw exception. 
        /// If responce it is correct, execute it
        /// </summary>
        /// <param name="message"></param>
        internal void sendMessage(IMessage message)
        {
            try
            {
                //send message
                communication.SendMessage(message);

                //receive message
                IMessage response = MessageFactory.createMessage();
                communication.ReceiveMessage(response);

                //check messageType
                List<IMessageContainer> responseContainers = response.GetMessageContainers();
                switch (responseContainers[0].getContainerType())
                {
                    case EMessageSymbols.msgTypeInformation:
                        executeInformationMessage(responseContainers);
                        break;

                    case EMessageSymbols.msgTypeError:
                        executeErrorMessage(responseContainers);
                        break;

                    default:
                        //TODO: log data and throw exception
                        break;
                }

            }
            catch (Exception ex)
            {
                //TODO: log data 

                throw;
            }
        }

        //information
        private void executeInformationMessage(List<IMessageContainer> informationContainers)
        {
            foreach (Information information in informationContainers)
            {
                foreach (InformationObject setting in information.settings)
                {
                    //TODO:
                }
            }
        }

        //error from response
        private void executeErrorMessage(List<IMessageContainer> errorContainers)
        {
            foreach (Error error in errorContainers)
            {
                //TODO: log data and throw exception
            }
        }
    }
}

