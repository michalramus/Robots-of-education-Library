using System;

//error generated, when model of device in setup incorrectly

namespace ROELibrary
{
    public class DeviceModelIncorrectSetupException : Exception
    {
        public DeviceModelIncorrectSetupException()
        {
        }

        public DeviceModelIncorrectSetupException(string message)
            : base(message)
        {
        }

        public DeviceModelIncorrectSetupException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
