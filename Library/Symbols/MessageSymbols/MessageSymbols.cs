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

                {EMessageSymbols.msgTypeError, "err"},

                {EMessageSymbols.msgTypeInformation, "info"},
            }
        );
    }
}
