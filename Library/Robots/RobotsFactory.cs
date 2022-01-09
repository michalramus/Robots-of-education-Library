using System;

namespace ROELibrary
{
    class RobotsFactory
    {
        static Func<VRobotModel, IRobotDevice> carDeviceCreator = (VRobotModel robotModel) => { return new CarDevice(robotModel, MessageFactory.createMessage, MessageContainerResolver.GetMessageContainerType); };

        static public Func<VRobotModel, IRobotDevice> getDeviceCreator(ERobotsSymbols deviceType)
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

        static public void updateDeviceCreators(ERobotsSymbols creatorType, Func<VRobotModel, IRobotDevice> creator)
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
