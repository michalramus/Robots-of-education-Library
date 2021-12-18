using System;
using System.Collections.Generic;

namespace ROELibrary
{
    class MessageType
    {
        public Symbols<EMessageType> symbols = new Symbols<EMessageType>(
            new Dictionary<EMessageType, string>
            {
                {EMessageType.msgTypeConfig, "conf"},

                {EMessageType.msgTypeTask, "task"},

                {EMessageType.msgTypeError, "err"},

                {EMessageType.msgTypeInformation, "info"},
            }
        );
    }
}
