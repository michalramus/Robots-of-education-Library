using System;
using ROELibrary;

namespace UnitTests
{
    abstract class MessageFactoryMock
    {
        public abstract IMessage GetMessageContainerType(EMessageSymbols messageType);
    }
}
