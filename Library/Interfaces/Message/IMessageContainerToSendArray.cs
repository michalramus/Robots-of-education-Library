using Newtonsoft.Json.Linq;

namespace ROELibrary
{
    interface IMessageContainerToSendArray : IMessageContainer //object that can be sended
    {
        JArray serializeToJsonObject();
    }
}
