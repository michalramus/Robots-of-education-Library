namespace ROELibrary
{
    interface ICommunication
    {

        void SendMessage(IMessage message);
        IMessage ReceiveMessage(IMessage emptyMessage);
    }
}

