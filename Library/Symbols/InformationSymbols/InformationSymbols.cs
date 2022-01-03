using System.Collections.Generic;

namespace ROELibrary
{
    class InformationSymbols
    {
        public static Symbols<EInformationSymbols> symbols = new Symbols<EInformationSymbols>(
            new Dictionary<EInformationSymbols, string>
            {
                {EInformationSymbols.getFirmwareVersion, "firmVer"}
            }
        );
    }
}
