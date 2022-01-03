using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ROELibrary
{
    class Device : VMessageContainer, IMessageContainerToSend
    {
        public Device()
        {
            devID = null;
            devType = null;

            pins = new List<uint>();
        }

        public List<uint> pins { get; set; } //pins used by this device

        public EMessageSymbols getContainerType()
        {
            return EMessageSymbols.contTypeDevice;
        }

        public JObject serializeToJsonObject()
        {
            if (isSet() == false)
            {
                MsgContainerNotSetException ex = new MsgContainerNotSetException("Device container is not correctly set");
                ex.Data.Add("devID", devID);
                ex.Data.Add("devType", devType);
                ex.Data.Add("pins", pins.ToArray());

                throw ex;
            }

            //serialize Json object
            JObject deviceObject = new JObject();
            deviceObject[RobotsSymbols.symbols.getValue(ERobotsSymbols.devType)] = RobotsSymbols.symbols.getValue((ERobotsSymbols)devType);
            deviceObject[RobotsSymbols.symbols.getValue(ERobotsSymbols.devID)] = devID;

            JArray jPins = new JArray();
            foreach (int pin in pins)
            {
                jPins.Add(pin);
            }
            deviceObject[RobotsSymbols.symbols.getValue(ERobotsSymbols.pins)] = jPins;

            return deviceObject;
        }

        private bool isSet() //check is every part of object was set
        {
            if (devType == null)
            {
                return false;
            }

            if (devID == null)
            {
                return false;
            }

            if (pins.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
