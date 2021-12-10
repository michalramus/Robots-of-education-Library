namespace ROELibrary
{
    public interface ISerialPort
    {
    string ReadLine();
    int BytesToRead();
    void Write(string test);
    }

}
