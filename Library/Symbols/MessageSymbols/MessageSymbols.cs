using System;
using System.Collections.Generic;

namespace ROELibrary
{
    class MessageSymbols
    {
        public static Symbols<EMessageSymbols> symbols = new Symbols<EMessageSymbols>(
            new Dictionary<EMessageSymbols, string>
            {
                {EMessageSymbols.startEndMessage, "x"}, //it has to have maximally 1 character
                {EMessageSymbols.messageType, "type"},

                {EMessageSymbols.msgTypeConfig, "conf"},
                {EMessageSymbols.contTypeDevice, "dev"},

                {EMessageSymbols.msgTypeTask, "task"},
                {EMessageSymbols.contTypeTask, "tasks"},

                {EMessageSymbols.msgTypeError, "error"},
                {EMessageSymbols.errorType, "errorType"},
                {EMessageSymbols.errorMessage, "errMsg"},
                {EMessageSymbols.errorValue, "errValue"},
                {EMessageSymbols.firmwareVersion, "firmVer"},

                {EMessageSymbols.msgTypeInformation, "info"},
                {EMessageSymbols.contTypeInformation, "data"},
                {EMessageSymbols.getSetting, "get"},
                {EMessageSymbols.setSetting, "set"}
            }
        );
    }
}
