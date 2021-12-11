namespace ROELibrary
{
    interface ISerialPort
    {
        string ReadLine();
        int BytesToRead();
        void Write(string test);
    }

}
