using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ROELibrary
{
    class Task : VMessageContainer, IMessageContainerToSend
    {
        public EMessageSymbols getContainerType()
        {
            return EMessageSymbols.contTypeTask;
        }

        public ERobotsSymbols? task { get; set; } = null;
        public int extraValuesNumber { get; private set; } = 0;
        public Dictionary<ERobotsSymbols, string> extraValues { get; private set; } = new Dictionary<ERobotsSymbols, string>();

        public void AddExtraValue(ERobotsSymbols valueID, string value)
        {
            extraValues.Add(valueID, value);
            extraValuesNumber++;
        }

        private bool isSet()
        {
            if (devType == null)
            {
                return false;
            }

            if (devID == null)
            {
                return false;
            }

            if (task == null)
            {
                return false;
            }

            return true;
        }

        public JObject serializeToJsonObject()
        {
            JObject taskObject = new JObject();

            if (isSet() == false)
            {
                MsgContainerNotSetException ex = new MsgContainerNotSetException("Task container is not correctly set");
                ex.Data.Add("devID", devID);
                ex.Data.Add("devType", devType);
                ex.Data.Add("task", task);
                ex.Data.Add("extraValues", string.Join("", extraValues));

                throw ex;
            }

            // serialize to Json
            taskObject[RobotsSymbols.symbols.getValue(ERobotsSymbols.devType)] = RobotsSymbols.symbols.getValue((ERobotsSymbols)devType);
            taskObject[RobotsSymbols.symbols.getValue(ERobotsSymbols.devID)] = devID;
            taskObject[RobotsSymbols.symbols.getValue(ERobotsSymbols.task)] = RobotsSymbols.symbols.getValue((ERobotsSymbols)task);
            taskObject[RobotsSymbols.symbols.getValue(ERobotsSymbols.extraValuesNumber)] = extraValuesNumber;

            //rewrite extra values to JArray
            JArray jExtraValues = new JArray();
            foreach (KeyValuePair<ERobotsSymbols, string> extraValue in extraValues)
            {
                JArray jExtraValue = new JArray();

                jExtraValue.Add(RobotsSymbols.symbols.getValue(extraValue.Key));
                jExtraValue.Add(extraValue.Value);

                jExtraValues.Add(jExtraValue);
            }

            taskObject[RobotsSymbols.symbols.getValue(ERobotsSymbols.extraValues)] = jExtraValues;

            return taskObject;
        }
    }
}
