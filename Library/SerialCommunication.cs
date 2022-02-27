using System;

namespace ROELibrary
{
    class SerialCommunication : ICommunication
    {
        ISerialPort _serialPort;

        public SerialCommunication(ISerialPort serialPort)
        {
            _serialPort = serialPort;
        }

        public void SendMessage(IMessage message)
        {
            string json = message.SerializeToJson();

            //add startEndMessage symbol to message
            json = MessageSymbols.symbols.getValue(EMessageSymbols.startEndMessage) + json + MessageSymbols.symbols.getValue(EMessageSymbols.startEndMessage);

            try
            {
                _serialPort.Write(json);
            }
            catch (InvalidOperationException ex)
            {
                var ex2 = new InvalidOperationException("Serial port is not open", ex);
                throw ex2;
            }
        }

        public IMessage ReceiveMessage(IMessage emptyMessage)
        {
            //TODO: use IoC container
            string json = "";
            bool messageReceived = false;

            while (messageReceived == false)
            {
                if (_serialPort.IsOpen() == true)
                {
                    //read message
                    if (_serialPort.BytesToRead() >= 2)
                    {
                            json = _serialPort.ReadLine();
                            messageReceived = true;
                        
                    }
                }
                else
                {
                    throw new InvalidOperationException("Serial port is not open");
                }
            }

            //remove every enter characters
            json = json.Replace("\r", "")
                        .Replace("\n", "");

            //check if message was correctly received
            if ((json[0] == MessageSymbols.symbols.getValue(EMessageSymbols.startEndMessage)[0]) && (json[json.Length - 1] == MessageSymbols.symbols.getValue(EMessageSymbols.startEndMessage)[0]))
            {
                json = json.TrimStart(MessageSymbols.symbols.getValue(EMessageSymbols.startEndMessage)[0]);
                json = json.TrimEnd(MessageSymbols.symbols.getValue(EMessageSymbols.startEndMessage)[0]);
            }
            else
            {
                var ex = new IncorrectMessageException("Received message is invalid: doesn't have startEndMessage symbol");
                ex.Data.Add("json", json);

                throw ex;
            }

            emptyMessage.DeserializeFromJson(json);

            return emptyMessage;
        }
    }
}
