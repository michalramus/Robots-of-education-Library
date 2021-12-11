namespace ROELibrary
{
    interface ICommunication
    {

        void SendMessage(IMessage message);
        string ReceiveMessage();
    }
}

