using System;

namespace ROELibrary
{
    class MessageFactory
    {
        static Func<IMessage> _messageCreator = () => new Message();

        public static void updateMessageCreator(Func<IMessage> messageCreator)
        {
            _messageCreator = messageCreator;
        }

        public static Func<IMessage> getMessageCreator()
        {
            return _messageCreator;
        }
    }
}
