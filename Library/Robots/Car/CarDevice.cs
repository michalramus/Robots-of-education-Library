using System;

namespace ROELibrary
{
    class CarDevice : ICarDevice
    {
        //properties
        uint id = 0;
        uint[] pins = null; // [en1][en2][en3][en4][speedControl-analogPin]
        decimal carGoTime = 0; // time when car ride 1 meter
        decimal carTurnTime = 0; // time when car turn 90 degrees
                                 //TODO: create unit tests

        //creators
        Func<IMessage> _createMessage = null;
        Func<EMessageSymbols, Func<IMessageContainer>> _createMessageContainer = null;
        Action<IMessage> _sendMessage = null;

        public CarDevice(VRobotModel carModel, Func<IMessage> createMessage, Func<EMessageSymbols, Func<IMessageContainer>> createMessageContainer, Action<IMessage> sendMessage)
        {
            //set creators
            _createMessage = createMessage;
            _createMessageContainer = createMessageContainer;
            _sendMessage = sendMessage;

            setDevice(carModel);
        }

        private void setDevice(VRobotModel model)
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

            if (pins.Length != 5)
            {
                //TODO: throw exception
            }

            //set properties
            id = (uint)carModel.id;
            pins = carModel.pins;
            carGoTime = (decimal)carModel.carGoTime;
            carTurnTime = (decimal)carModel.carTurnTime;
        }

        private void sendConfigMsg()
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
            _sendMessage(msg);
        }

        public void goForward(uint distance)
        {
            //create message
            IMessage msg = _createMessage();
            Task task = _createMessageContainer(EMessageSymbols.msgTypeTask)() as Task;

            //set device
            task.devID = id;
            task.devType = ERobotsSymbols.car;
            task.task = ERobotsSymbols.taskCarGo;
            task.AddExtraValue(ERobotsSymbols.valCarDistance, distance.ToString());

            //send message
            msg.addMsgContainer(task);
            _sendMessage(msg);

        }

        public void rotate(uint angle)
        {
            // create message
            IMessage msg = _createMessage();
            Task task = _createMessageContainer(EMessageSymbols.msgTypeTask)() as Task;

            //set device
            task.devID = id;
            task.devType = ERobotsSymbols.car;
            task.task = ERobotsSymbols.taskCarTurn;
            task.AddExtraValue(ERobotsSymbols.valCarDistance, angle.ToString());

            //send message
            msg.addMsgContainer(task);
            _sendMessage(msg);

        }
        public void setSpeed(uint speed)
        {
            // create message
            IMessage msg = _createMessage();
            Task task = _createMessageContainer(EMessageSymbols.msgTypeTask)() as Task;

            //set device
            task.devID = id;
            task.devType = ERobotsSymbols.car;
            task.task = ERobotsSymbols.nothingToDo;
            task.AddExtraValue(ERobotsSymbols.valCarSpeed, speed.ToString());

            //send message
            msg.addMsgContainer(task);
            _sendMessage(msg);

        }

        public void setRotationalSpeed(uint speed)
        {
            // create message
            IMessage msg = _createMessage();
            Task task = _createMessageContainer(EMessageSymbols.msgTypeTask)() as Task;

            //set device
            task.devID = id;
            task.devType = ERobotsSymbols.car;
            task.task = ERobotsSymbols.nothingToDo;
            task.AddExtraValue(ERobotsSymbols.valCarRotationalSpeed, speed.ToString());

            //send message
            msg.addMsgContainer(task);
            _sendMessage(msg);
        }

    }
}
