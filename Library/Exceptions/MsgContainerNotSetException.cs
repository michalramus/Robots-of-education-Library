using System;

//error generated, when message container is not fully set

namespace ROELibrary
{
    public class MsgContainerNotSetException : Exception
    {
        public MsgContainerNotSetException()
        {
        }

        public MsgContainerNotSetException(string message)
            : base(message)
        {
        }

        public MsgContainerNotSetException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
