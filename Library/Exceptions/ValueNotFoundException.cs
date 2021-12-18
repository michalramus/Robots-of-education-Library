using System;

//error generated, when value can't be found in symbolsBase

namespace ROELibrary
{
    public class ValueNotFoundException : Exception
    {
        public ValueNotFoundException()
        {
        }

        public ValueNotFoundException(string message)
            : base(message)
        {
        }

        public ValueNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
