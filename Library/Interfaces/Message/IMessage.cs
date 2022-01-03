using System.Collections.Generic;

namespace ROELibrary
{
    interface IMessage
    {
        string SerializeToJson(); //convert message to json
        void DeserializeFromJson(string json); //convert json to message
        void addMsgContainer(IMessageContainer messageContainer);
        List<IMessageContainer> GetMessageContainers();

    }
}
