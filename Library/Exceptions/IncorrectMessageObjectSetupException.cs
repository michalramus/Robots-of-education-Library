using System;

//error generated, when message object is set up incorrectly (e.g. no message container is added during serialization)

namespace ROELibrary
{
    public class IncorrectMessageObjectSetupException : Exception
    {
        public IncorrectMessageObjectSetupException()
        {
        }

        public IncorrectMessageObjectSetupException(string message)
            : base(message)
        {
        }

        public IncorrectMessageObjectSetupException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
