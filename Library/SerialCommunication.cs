using System;

namespace ROELibrary
{
    class SerialCommunication : ICommunication
    {
        ISerialPort _serialPort;

        //TODO: set JsonDeserializer
        public SerialCommunication(ISerialPort serialPort)
        {
            _serialPort = serialPort;
        }

        /// <summary>
        /// messageJson parameter has to be convertible to Json
        /// </summary>
        /// <param name="messageJson"></param>
        public void SendMessage(IMessage messageJson)
        {
            //TODO:
        }



        private void SendMessage(string message)
        {
            //add startEndMessage symbol to message
            message.Insert(0, SymbolsBase.GetSymbol(SymbolsIDs.startEndMessage));
            message += SymbolsBase.GetSymbol(SymbolsIDs.startEndMessage);

            try
            {
                _serialPort.Write(message);
            }
            catch (InvalidOperationException e)
            {
                //TODO: handle exception
            }
            catch (Exception e)
            {
                //TODO: handle exception
            }
        }

        public string ReceiveMessage()
        {
            string message = "";
            bool messageReceived = false;

            while (messageReceived == false)
            {
                if (_serialPort.IsOpen() == true)
                {
                    //read message
                    if (_serialPort.BytesToRead() > 0)
                    {
                            message = _serialPort.ReadLine();
                            messageReceived = true;
                        
                    }
                }
                else
                {
                    throw new InvalidOperationException();
                    //TODO: add more information to exception
                }
            }

            //check if message was correctly received
            if ((message[0] == SymbolsBase.GetSymbol(SymbolsIDs.startEndMessage)[0]) && (message[message.Length - 1] == SymbolsBase.GetSymbol(SymbolsIDs.startEndMessage)[0]))
            {
                message = message.TrimStart(SymbolsBase.GetSymbol(SymbolsIDs.startEndMessage)[0]);
                message = message.TrimEnd(SymbolsBase.GetSymbol(SymbolsIDs.startEndMessage)[0]);
            }
            else
            {
                //TODO: throw exception
            }

            return message;
        }
    }
}
