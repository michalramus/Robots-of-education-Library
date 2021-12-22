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


                {EMessageSymbols.msgTypeConfig, "conf"},

                {EMessageSymbols.msgTypeTask, "task"},

                {EMessageSymbols.msgTypeError, "error"},
                {EMessageSymbols.errorType, "errorType"},
                {EMessageSymbols.errorMessage, "errMsg"},
                {EMessageSymbols.errorValue, "errValue"},
                {EMessageSymbols.firmwareVersion, "firmVer"},

                {EMessageSymbols.msgTypeInformation, "info"},
            }
        );
    }
}
