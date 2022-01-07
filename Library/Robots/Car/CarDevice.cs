using System;

namespace ROELibrary
{
    class CarDevice : ICarDevice
    {
        //properties
        uint id = 0;
        uint[] pins = new uint[5]; // [en1][en2][en3][en4][speedControl-analogPin]
        decimal carGoTime = 0; // time when car ride 1 meter
        decimal carTurnTime = 0; // time when car turn 90 degrees


        //creators
        Func<IMessage> _createMessage = null;
        Func<EMessageSymbols, Func<IMessageContainer>> _createMessageContainer = null;

        CarDevice(VRobotModel carModel, Func<IMessage> createMessage, Func<EMessageSymbols, Func<IMessageContainer>> createMessageContainer)
        {
            //set creators
            _createMessage = createMessage;
            _createMessageContainer = createMessageContainer;

            setDevice(carModel);
        }

        void setDevice(VRobotModel model)
        {
            //validate model
            if (model.getModelType() != ERobotsSymbols.car)
            {
                //TODO: throw exception
            }

            CarModel carModel = (CarModel)model;

            if ((carModel.id == null) || (carModel.pins == null) || (carModel.carGoTime == null) || (carModel.carTurnTime == null))
            {
                //TODO: throw exception
            }

            //set properties
            id = (uint)carModel.id;
            pins = carModel.pins;
            carGoTime = (decimal)carModel.carGoTime;
            carTurnTime = (decimal)carModel.carTurnTime;
        }

        void sendConfigMsg()
        {
            //create message
            IMessage msg = _createMessage();
            Device device = _createMessageContainer(EMessageSymbols.msgTypeConfig)() as Device;

            //set device
            device.devID = id;
            device.devType = ERobotsSymbols.car;
            foreach (uint pin in pins)
            {
                device.pins.Add(pin);
            }

            //send message
            msg.addMsgContainer(device);
            //TODO: send message
        }

        public void goForward(uint distance)
        {
            
        }

        public void rotate(uint angle)
        {
            
        }

        public void setRotationalSpeed(uint speed)
        {
            
        }

        public void setSpeed(uint speed)
        {
            
        }
    }
}
