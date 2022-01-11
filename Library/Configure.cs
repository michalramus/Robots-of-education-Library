namespace ROELibrary
{
    //model with properties to configure Robot class
    public class Configure
    {
        //serialPort
        public string serialPortName = null;
        public uint? baudRate = 9600;

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
            
            return true;
        }
    }
}
