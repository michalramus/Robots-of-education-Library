using System;
using System.Collections.Generic;

namespace ROELibrary
{
    class RobotsSymbols
    {
        public static Symbols<ERobotsSymbols> symbols = new Symbols<ERobotsSymbols>(
            new Dictionary<ERobotsSymbols, string>
            {
                //universal symbols
                {ERobotsSymbols.devType, "type"},
                {ERobotsSymbols.devID, "ID"},
                {ERobotsSymbols.pins, "pins"},

                //symbols for concrete robot
                
                //car
                {ERobotsSymbols.car, "car"},
            }
        );
    }
}
