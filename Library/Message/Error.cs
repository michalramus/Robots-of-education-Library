using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ROELibrary
{
    class Error : IMessageContainerToReceive
    {
        public EErrorSymbols errorType { get; private set; }
        public string message { get; private set; }
        public string value { get; private set; } //data that might have to caused the error
        public string firmwareVersion { get; private set; }
        public EMessageSymbols getContainerType()
        {
            return EMessageSymbols.contTypeError;
        }

        public void deserializeFromJsonObject(JObject json) //deserialize error Object from Json
        {
            try
            {
                //deserialize json
                errorType = ErrorSymbols.symbols.getKey(json[MessageSymbols.symbols.getValue(EMessageSymbols.errorType)].ToString());
                message = json[MessageSymbols.symbols.getValue(EMessageSymbols.errorMessage)].ToString();
                value = json[MessageSymbols.symbols.getValue(EMessageSymbols.errorValue)].ToString();
                firmwareVersion = json[MessageSymbols.symbols.getValue(EMessageSymbols.firmwareVersion)].ToString();
            }
            catch (NullReferenceException ex)
            {
                var ex2 = new IncorrectMessageException("Error message has missing key", ex);
                ex2.Data.Add("json", json.ToString());

                throw ex2;
            }
            catch (ValueNotFoundException ex)
            {
                var ex2 = new IncorrectMessageException("Error type is incorrect", ex);
                ex2.Data.Add("json", json.ToString().Replace(" ", "").Replace("\n", ""));

                throw ex2;
            }
        }
    }
}
