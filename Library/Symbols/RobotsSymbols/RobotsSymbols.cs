using System;
using System.Collections.Generic;

namespace ROELibrary
{
    class RobotsSymbols
    {
        public Symbols<ERobotsSymbols> symbols = new Symbols<ERobotsSymbols>(
            new Dictionary<ERobotsSymbols, string>
            {
                {ERobotsSymbols.car, "car"},
            }
        );
    }
}
