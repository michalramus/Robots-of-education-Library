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
                {ERobotsSymbols.devType, "devType"},
                {ERobotsSymbols.devID, "ID"},

                //config
                {ERobotsSymbols.pins, "pins"},

                //task
                {ERobotsSymbols.task, "task"},
                {ERobotsSymbols.extraValuesNumber, "exValNum"},
                {ERobotsSymbols.extraValues, "exVal"},
                {ERobotsSymbols.nothingToDo, "nothing"},


                //symbols for concrete robot
                
                //car
                {ERobotsSymbols.car, "car"},
                {ERobotsSymbols.taskCarGo, "tCarGo"},
                {ERobotsSymbols.taskCarTurn, "tCarTurn"},

                {ERobotsSymbols.valCarSpeed, "vCarSpeed"},
                {ERobotsSymbols.valCarDistance, "vCarDist"},
                {ERobotsSymbols.valCarRotationalSpeed, "vCarRotSpeed"},
                {ERobotsSymbols.valCarAngle, "vCarAngle"},

                {ERobotsSymbols.valCarGoTime, "vCarGoTime"},
                {ERobotsSymbols.valCarTurnTime, "vCarTurnTime"},
            }
        );
    }
}
