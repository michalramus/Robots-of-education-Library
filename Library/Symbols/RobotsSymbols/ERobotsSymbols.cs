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
    
    //calibration
    valCarImpulsesPerRotation, //impulses sent by hall sensor per one wheel rotation
    valCarCircumference, //circumference of wheel in centimeters


}
