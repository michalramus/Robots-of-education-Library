using Newtonsoft.Json.Linq;

namespace ROELibrary
{
    interface IMessageContainerToReceive : IMessageContainer //object that can be received
    {
        void deserializeFromJsonObject(JObject json);
    }
}
