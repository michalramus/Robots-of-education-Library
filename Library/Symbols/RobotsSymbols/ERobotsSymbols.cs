enum ERobotsSymbols
{
    //Types of robots and values of it

    //universal symbols
    devType,
    devID,

    //config
    pins,

    //task
    task,
    extraValuesNumber,
    extraValues,
    nothingToDo, //special task to send only extra values

    //symbols for concrete robot

    //CAR
    car,
    //task
    taskCarGo,
    taskCarTurn,
    //values
    valCarSpeed,
    valCarDistance,
    valCarRotationalSpeed,
    valCarAngle,
    //TODO: delete calibration symbols
    //calibration
    valCarGoTime, //time when car ride 1 meter
    valCarTurnTime, //time when car turn 90 degrees


}
