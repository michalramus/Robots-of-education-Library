using System;

namespace ROELibrary
{
    class RobotsFactory
    {
        //creators
        static Func<VRobotModel, Action<IMessage>, IRobotDevice> carDeviceCreator = (VRobotModel robotModel, Action<IMessage> sendMessage) => { return new CarDevice(robotModel, MessageFactory.createMessage, MessageContainerResolver.GetMessageContainerType, sendMessage); };


        static public Func<VRobotModel, Action<IMessage>, IRobotDevice> getDeviceCreator(ERobotsSymbols deviceType)
        {
            switch (deviceType)
            {
                case ERobotsSymbols.car:
                    return carDeviceCreator;

                default:
                    var ex = new ArgumentException("Creator for a robot type doesn't exist");
                    ex.Data["deviceType"] = deviceType;
                    throw ex;
            }
        }

        static public void updateDeviceCreators(ERobotsSymbols creatorType, Func<VRobotModel, Action<IMessage>, IRobotDevice> creator)
        {
            switch (creatorType)
            {
                case ERobotsSymbols.car:
                    carDeviceCreator = creator;
                    break;

                default:
                    var ex = new ArgumentException("Can't change creator for selected robot, because it doesn't exist");
                    ex.Data["creatorType"] = creatorType;
                    throw ex;
            }
        }
    }
}
