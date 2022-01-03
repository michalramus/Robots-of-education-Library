using System.Collections.Generic;

namespace ROELibrary
{
    class ErrorSymbols
    {
        public static Symbols<EErrorSymbols> symbols = new Symbols<EErrorSymbols>(
            new Dictionary<EErrorSymbols, string>
            {
                {EErrorSymbols.incorrectPinout, "incorrectPinout"},
            }
        );
    }
}
