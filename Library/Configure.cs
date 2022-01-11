namespace ROELibrary
{
    //model with properties to configure Robot class
    public class Configure
    {
        //serialPort
        public string serialPortName = null;
        public uint? baudRate = 9600;
        public uint? readBufferSize = 4096;
        public uint? writeBufferSize = 4096;

        /// <summary>
        /// Use different serial port. null - use standard serial port
        /// only for development
        /// </summary>
        internal ISerialPort serialPort = null;
        
        /// <summary>
        /// in milliseconds
        /// </summary>
        public uint? readTimeout = 2000;

        //loging
        public bool logToDatabase = true;


        internal bool isModelSetup()
        {
            if (serialPortName == null)
            {
                return false;
            }
            else if (baudRate == null)
            {
                return false;
            }
            else if (readBufferSize == null)
            {
                return false;
            }
            else if (writeBufferSize == null)
            {
                return false;
            }
            else if (readTimeout == null)
            {
                return false;
            }
            
            return true;
        }
    }
}
