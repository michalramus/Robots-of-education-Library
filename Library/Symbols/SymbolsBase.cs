using System;
using System.Collections.Generic;

namespace ROELibrary
{
    static class SymbolsBase
    {
        private static string[] symbols = {
            "x" //it has to have maximally 1 character
            };

        public static string GetSymbol(SymbolsIDs ID)
        {
            try
            {
                return symbols[(int)ID];
            }
            catch(Exception e)
            {
                //TODO: log error

                throw e;
            }
        }

        public static int GetID(string symbol)
        {
            for (int i = 0; i < symbols.Length; i++)
            {
                if (symbol == symbols[i])
                {
                    return i;
                }
            }

            //TODO: log error and add more information
            var Exception = new KeyNotFoundException();

            throw Exception;
        }
    }
}
