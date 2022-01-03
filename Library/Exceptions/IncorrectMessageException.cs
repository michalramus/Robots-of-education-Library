using System;

//error generated, when received message is in incorrect format

namespace ROELibrary
{
    public class IncorrectMessageException : Exception
    {
        public IncorrectMessageException()
        {
        }

        public IncorrectMessageException(string message)
            : base(message)
        {
        }

        public IncorrectMessageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
