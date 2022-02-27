using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ROELibrary
{
    class Information : IMessageContainerToSendArray, IMessageContainerToReceiveArray
    {
        public List<InformationObject> settings { get; private set; } = new List<InformationObject>(); //list of all settings in this information object

        public EMessageSymbols getContainerType()
        {
            return EMessageSymbols.contTypeInformation;
        }

        /// <summary>
        /// get setting from the robot
        /// </summary>
        /// <param name="setting"></param>
        public void addSetting(EInformationSymbols setting)
        {
            InformationObject informationObject = new InformationObject();

            informationObject.setting = setting;
            informationObject.settingStatus = EMessageSymbols.getSetting;

            settings.Add(informationObject);
        }

        /// <summary>
        /// set setting to the robot
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="value"></param>
        public void addSetting(EInformationSymbols setting, string value)
        {
            InformationObject informationObject = new InformationObject();

            informationObject.setting = setting;
            informationObject.settingStatus = EMessageSymbols.setSetting;
            informationObject.value = value;

            settings.Add(informationObject);
        }


        public JArray serializeToJsonArray()
        {
            if (settings.Count == 0)
            {
                throw new MsgContainerNotSetException("Information container doesn't have any settings");
            }

            JArray jsonArray = new JArray();

            foreach (InformationObject setting in settings)
            {
                JArray settingArray = new JArray();

                settingArray.Add(InformationSymbols.symbols.getValue(setting.setting));

                //check if setting is to set or get
                if (setting.settingStatus == EMessageSymbols.getSetting)
                {
                    settingArray.Add(MessageSymbols.symbols.getValue((EMessageSymbols)setting.settingStatus));
                }
                else
                {
                    settingArray.Add(MessageSymbols.symbols.getValue((EMessageSymbols)setting.settingStatus));
                    settingArray.Add(setting.value);
                }

                jsonArray.Add(settingArray);
            }

            return jsonArray;
        }

        public void deserializeFromJsonArray(JArray jsonArray)
        {
            foreach (var settingArray in jsonArray)
            {
                InformationObject informationObject = new InformationObject();

                try
                {
                informationObject.setting = InformationSymbols.symbols.getKey(settingArray[0].ToString());
                informationObject.value = settingArray[1].ToString();
                }
                catch (ValueNotFoundException ex)
                {
                    var ex2 = new IncorrectMessageException("Information setting is incorrect", ex);
                    ex2.Data.Add("json", jsonArray.ToString().Replace(" ", "").Replace("\n", "").Replace("\r", ""));

                    throw ex2;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    var ex2 = new IncorrectMessageException("Information message has missing key", ex);
                    ex2.Data.Add("json", jsonArray.ToString().Replace(" ", "").Replace("\n", "").Replace("\r", ""));

                    throw ex2;
                }

                settings.Add(informationObject);
            }
        }
    }
}
