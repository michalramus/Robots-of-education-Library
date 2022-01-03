using System;
using ROELibrary;

namespace UnitTests
{
    abstract class MessageContainerResolverMock
    {
        public abstract Func<IMessageContainer> GetMessageContainerType(EMessageSymbols messageType);
    }
}
