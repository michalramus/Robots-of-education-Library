namespace ROELibrary
{
    public interface ICommunication
    {

        void SendMessage(IMessage message);
        string ReceiveMessage();
    }
}

