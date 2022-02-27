using Newtonsoft.Json.Linq;

namespace ROELibrary
{
    interface IMessageContainerToReceiveArray : IMessageContainer //object that can be received
    {
        void deserializeFromJsonArray(JArray json);
    }
}
