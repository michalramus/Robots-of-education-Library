using Newtonsoft.Json.Linq;

namespace ROELibrary
{
    interface IMessageContainerToReceiveArray : IMessageContainer //object that can be received
    {
        void deserializeFromJsonObject(JArray json);
    }
}
