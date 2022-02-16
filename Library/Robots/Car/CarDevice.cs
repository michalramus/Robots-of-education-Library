using System;

namespace ROELibrary
{
    class CarDevice : ICarDevice
    {
        //properties
        uint id = 0;
        uint[] pins = null; // [en1][en2][en3][en4][speedControl-analogPin]
        uint impulsesPerRotation = 0; // impulses sent by hall sensor per one wheel rotation
        uint circumference = 0; // circumference of wheel in centimeters


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
            sendConfigMsg();
        }

        private void setDevice(VRobotModel model)
        {
            //validate model
            if (model.getModelType() != ERobotsSymbols.car)
            {
                var ex = new DeviceModelIncorrectSetupException("Incorrect DeviceModel: expected CarModel");
                ex.Data["modelType"] = model.getModelType();
                ex.Data["exceptedModelType"] = ERobotsSymbols.car;
                ex.Data["model"] = model.ToString();
                throw ex;
            }

            CarModel carModel = (CarModel)model;

            if ((carModel.id == null) || (carModel.pins == null) || (carModel.impulsesPerRotation == null) || (carModel.circumference == null))
            {
                var ex = new DeviceModelIncorrectSetupException("Some of properties in model are null");
                ex.Data["id"] = carModel.id;
                ex.Data["type"] = ERobotsSymbols.car;
                ex.Data["pins"] = carModel.pins;
                ex.Data["impulsesPerRotation"] = carModel.impulsesPerRotation;
                ex.Data["circumference"] = carModel.circumference;

                throw ex;
            }

            if (carModel.pins.Length != 5)
            {
                var ex = new DeviceModelIncorrectSetupException("Car device model must have defined 5 pins");
                ex.Data.Add("id", carModel.id);
                ex.Data["type"] = ERobotsSymbols.car;
                ex.Data["pins"] = carModel.pins;
                ex.Data["impulsesPerRotation"] = carModel.impulsesPerRotation;
                ex.Data["circumference"] = carModel.circumference;

                throw ex;
            }

            //set properties
            id = (uint)carModel.id;
            pins = carModel.pins;
            impulsesPerRotation = (uint)carModel.impulsesPerRotation;
            circumference = (uint)carModel.circumference;
        }

        private void sendConfigMsg()
        {
            //SEND CONFIG MESSAGE
            //create message
            IMessage confMsg = _createMessage();
            Device device = _createMessageContainer(EMessageSymbols.msgTypeConfig)() as Device;

            //set device
            device.devID = id;
            device.devType = ERobotsSymbols.car;
            foreach (uint pin in pins)
            {
                device.pins.Add(pin);
            }

            //send message
            confMsg.addMsgContainer(device);
            _sendMessage(confMsg);

            //SEND TASK MESSAGE to set extra values
            //create message
            IMessage taskMsg = _createMessage();
            Task task = _createMessageContainer(EMessageSymbols.msgTypeTask)() as Task;

            //set task
            task.devID = id;
            task.devType = ERobotsSymbols.car;
            task.task = ERobotsSymbols.nothingToDo;
            task.AddExtraValue(ERobotsSymbols.valCarImpulsesPerRotation, impulsesPerRotation.ToString());
            task.AddExtraValue(ERobotsSymbols.valCarCircumference, circumference.ToString());

            //send message
            taskMsg.addMsgContainer(task);
            _sendMessage(taskMsg);
        }

        public void goForward(uint distance)
        {
            go(distance, false);
        }

        public void goBackward(uint distance)
        {
            go(distance, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="direction">0 - go forward; 1 - go backward</param>
        private void go(uint distance, bool direction)
        {
            //create message
            IMessage msg = _createMessage();
            Task task = _createMessageContainer(EMessageSymbols.msgTypeTask)() as Task;

            //set device
            task.devID = id;
            task.devType = ERobotsSymbols.car;
            task.task = ERobotsSymbols.taskCarGo;
            task.AddExtraValue(ERobotsSymbols.valCarDistance, distance.ToString());

            if (direction == false)
            {
                task.AddExtraValue(ERobotsSymbols.valCarDirection, "forw");
            }
            else
            {
                task.AddExtraValue(ERobotsSymbols.valCarDirection, "back");
            }

            //send message
            msg.addMsgContainer(task);
            _sendMessage(msg);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="direction">0 - turn left; 1 - turn right</param>
        public void rotate(uint angle, bool direction)
        {
            // create message
            IMessage msg = _createMessage();
            Task task = _createMessageContainer(EMessageSymbols.msgTypeTask)() as Task;

            //set device
            task.devID = id;
            task.devType = ERobotsSymbols.car;
            task.task = ERobotsSymbols.taskCarTurn;
            task.AddExtraValue(ERobotsSymbols.valCarAngle, angle.ToString());

            if (direction == false)
            {
                task.AddExtraValue(ERobotsSymbols.valCarDirection, "left");
            }
            else
            {
                task.AddExtraValue(ERobotsSymbols.valCarDirection, "right");
            }

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
