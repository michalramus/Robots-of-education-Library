enum EMessageSymbols
{
    //Message and MessageContainers types and related with them symbols
    //msg - message
    //cont - container

    startEndMessage, //it has to have maximally 1 character
    messageType,

    msgTypeConfig,
    contTypeDevice,

    msgTypeTask,
    contTypeTask,

    msgTypeError,
    contTypeError, //don't have symbol
    errorType,
    errorMessage,
    errorValue,
    firmwareVersion,

    msgTypeInformation,
    contTypeInformation,

}
