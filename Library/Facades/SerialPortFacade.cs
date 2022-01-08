using System.IO.Ports;

namespace ROELibrary
{
    class SerialPortFacade : ISerialPort
    {
        SerialPort _serialPort;
        public SerialPortFacade(SerialPort serialPort)
        {
            _serialPort = serialPort;
        }
        public string ReadLine()
        {
            return _serialPort.ReadLine();
        }
        public int BytesToRead()
        {
            return _serialPort.BytesToRead;
        }
        public void Write(string test)
        {
            _serialPort.Write(test);
        }

        public bool IsOpen()
        {
            return _serialPort.IsOpen;
        }
    }
}
