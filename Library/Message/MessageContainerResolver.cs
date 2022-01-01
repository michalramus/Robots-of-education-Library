using System;

namespace ROELibrary
{
    class MessageContainerResolver
    {
        static Type config = typeof(Device);
        static Type task = typeof(Task);
        static Type error = typeof(Error);
        static Type information = typeof(Information);


        /// <summary>
        /// Change container type that will be return by MessageContainerResolver
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="messageContainer"></param>
        public static void updateContainerType(EMessageSymbols messageType, Type messageContainer)
        {
            switch (messageType)
            {
                case EMessageSymbols.msgTypeConfig:
                    MessageContainerResolver.config = messageContainer;
                    break;

                case EMessageSymbols.msgTypeTask:
                    MessageContainerResolver.task = messageContainer;
                    break;

                case EMessageSymbols.msgTypeError:
                    MessageContainerResolver.error = messageContainer;
                    break;

                case EMessageSymbols.msgTypeInformation:
                    MessageContainerResolver.information = messageContainer;
                    break;

                default:
                    throw new ArgumentException("Incorrect message type");
            }
        }

        public static Type GetMessageContainerType(EMessageSymbols messageType)
        {
            switch (messageType)
            {
                case EMessageSymbols.msgTypeConfig:
                    return config;

                case EMessageSymbols.msgTypeTask:
                    return task;

                case EMessageSymbols.msgTypeError:
                    return error;

                case EMessageSymbols.msgTypeInformation:
                    return information;

                default:
                    var ex = new IncorrectMessageException("Message type is incorrect");
                    ex.Data["messageType"] = messageType;

                    throw ex;
            }
        }
    }
}
