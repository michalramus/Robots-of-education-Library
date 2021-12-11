namespace ROELibrary
{
    interface ICommunication
    {

        void SendMessage(string message);
        string ReceiveMessage();
    }
}

