using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ROELibrary
{
    class Message : IMessage
    {
        Func<EMessageSymbols, Func<IMessageContainer>> createMessageContainer = MessageContainerResolver.GetMessageContainerType;
        public EMessageSymbols messageType { get; private set; }

        List<IMessageContainer> messageContainers = new List<IMessageContainer>();

        /// <summary>
        /// By default delegate is set to MessageContainerResolver.GetMessageContainerType
        /// </summary>
        /// <param name="getMessageContainerType"></param>
        public void setGetMessageContainerTypeDelegate(Func<EMessageSymbols, Func<IMessageContainer>> createMessageContainer)
        {
            this.createMessageContainer = createMessageContainer;
        }


        /// <summary>
        /// Message type is set by type of first added message container
        /// </summary>
        /// <param name="messageContainer"></param>
        public void addMsgContainer(IMessageContainer messageContainer)
        {
            if (messageContainers.Count == 0)
            {
                switch (messageContainer.getContainerType())
                {
                    case EMessageSymbols.contTypeDevice:
                        messageType = EMessageSymbols.msgTypeConfig;
                        break;

                    case EMessageSymbols.contTypeTask:
                        messageType = EMessageSymbols.msgTypeTask;
                        break;

                    case EMessageSymbols.contTypeError:
                        messageType = EMessageSymbols.msgTypeError;
                        break;
                    case EMessageSymbols.contTypeInformation:
                        messageType = EMessageSymbols.msgTypeInformation;
                        break;
                }

                messageContainers.Add(messageContainer);
                return; //break method if first container is added
            }

            //check if adding message container is the same type like others
            if (messageContainer.getContainerType() != messageContainers[0].getContainerType())
            {
                var ex = new IncorrectMessageObjectSetupException("Trying to add message container of different type than first added message container");
                ex.Data["messageType"] = messageType;
                ex.Data["addedContainerType"] = messageContainer.getContainerType();

                throw ex;
            }

            messageContainers.Add(messageContainer);
        }

        public List<IMessageContainer> GetMessageContainers()
        {
            return messageContainers;
        }

        public string SerializeToJson()
        {
            //check if message is empty
            if (messageContainers.Count == 0)
            {
                throw new IncorrectMessageObjectSetupException("Trying to serialize empty message object");
            }

            if ((messageType == EMessageSymbols.msgTypeConfig) || (messageType == EMessageSymbols.msgTypeTask)) //serialize task or config
            {
                JObject jsonObj = serializeJObject();

                string json = jsonObj.ToString();
                json = json.Replace("\n", "").Replace(" ", "").Replace("\r", "");

                return json;
            }
            else if (messageType == EMessageSymbols.msgTypeInformation) //serialize information
            {
                JObject jsonObj = serializeJArray();

                string json = jsonObj.ToString();
                json = json.Replace("\n", "").Replace(" ", "").Replace("\r", "");

                return json;
            }
            else
            {
                var ex = new IncorrectMessageObjectSetupException("Trying to serialize message that cannot be serialized to Json");
                ex.Data["messageType"] = messageType;

                throw ex;
            }

        }

        private JObject serializeJObject()
        {
            JObject jObject = new JObject();
            jObject[MessageSymbols.symbols.getValue(EMessageSymbols.messageType)] = MessageSymbols.symbols.getValue(messageType);

            //add json objects to array
            JArray objArray = new JArray();
            foreach (IMessageContainerToSend messageContainer in messageContainers)
            {
                objArray.Add(messageContainer.serializeToJsonObject());
            }

            jObject[MessageSymbols.symbols.getValue(messageContainers[0].getContainerType())] = objArray;

            return jObject;
        }

        private JObject serializeJArray()
        {
            JObject jObject = new JObject();
            jObject[MessageSymbols.symbols.getValue(EMessageSymbols.messageType)] = MessageSymbols.symbols.getValue(messageType);

            JArray settings = new JArray();
            foreach (IMessageContainerToSendArray messageContainer in messageContainers)
            {
                foreach (JArray setting in messageContainer.serializeToJsonArray()) //rewrite every setting to settings JArray
                {
                    settings.Add(setting);
                }
            }

            jObject[MessageSymbols.symbols.getValue(messageContainers[0].getContainerType())] = settings;

            return jObject;
        }

        public void DeserializeFromJson(string json)
        {
            JObject jsonObject = new JObject();

            //validate message
            try
            {
                jsonObject = JObject.Parse(json);
            }
            catch (JsonReaderException ex)
            {
                IncorrectMessageException ex2 = new IncorrectMessageException("Cannot deserialize received message", ex);
                ex2.Data.Add("json", json);

                throw ex2;
            }

            if (jsonObject.ContainsKey(MessageSymbols.symbols.getValue(EMessageSymbols.messageType)))
            {
                messageType = MessageSymbols.symbols.getKey(jsonObject[MessageSymbols.symbols.getValue(EMessageSymbols.messageType)].ToString());
            }
            else
            {
                IncorrectMessageException ex = new IncorrectMessageException("Message has missing message type key");
                ex.Data.Add("json", json);

                throw ex;
            }

            //deserialize message
            switch (messageType)
            {
                case EMessageSymbols.msgTypeError:
                    IMessageContainerToReceive error = createMessageContainer(messageType)() as IMessageContainerToReceive;

                    error.deserializeFromJsonObject(jsonObject);
                    messageContainers.Add(error);
                    break;

                case EMessageSymbols.msgTypeInformation:
                    try
                    {
                        messageContainers.Add(deserializeInformation(jsonObject, createMessageContainer(messageType)() as IMessageContainerToReceiveArray));
                    }
                    catch (IncorrectMessageException ex)
                    {
                        ex.Data["json"] = json;
                        throw;
                    }

                    break;
            }
        }

        private IMessageContainer deserializeInformation(JObject jsonObject, IMessageContainerToReceiveArray information)
        {
            JToken jsonArray;
            try
            {
                jsonArray = jsonObject[MessageSymbols.symbols.getValue(EMessageSymbols.contTypeInformation)];
            }
            catch (NullReferenceException ex)
            {
                var ex2 = new IncorrectMessageException("Information message has missing key", ex); 
                throw ex2;
            }

            JArray informationArray = jsonArray as JArray;
            if (informationArray == null)
            {
                var ex = new IncorrectMessageException("Information message is in incorrect format");
                throw ex;
            }

            information.deserializeFromJsonArray(informationArray);
            return information;
        }

    }
}
