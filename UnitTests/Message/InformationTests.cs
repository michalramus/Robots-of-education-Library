using Xunit;
using ROELibrary;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace UnitTests.Msg
{
    public class InformationTests
    {
        public static IEnumerable<object[]> Data_SerializeToJsonArray_SerializeInformation_ReturnCorrectJson() //TODO: add more tests
        {
            yield return new object[] { new List<List<object>>() { new List<object> {EInformationSymbols.getFirmwareVersion}}, "[[\"firmVer\",\"get\"]]" };
        }
        [Theory]
        [MemberData(nameof(Data_SerializeToJsonArray_SerializeInformation_ReturnCorrectJson))]
        public void SerializeToJsonArray_SerializeInformation_ReturnCorrectJson(List<List<object>> data, string json)
        {
            //Arrange
            Information information = new Information();

            //add settings to the information object
            foreach (List<object> list in data)
            {
                if (list.Count == 2)
                {
                    information.addSetting((EInformationSymbols)list[0], (string)list[1]);
                }
                else
                {
                    information.addSetting((EInformationSymbols)list[0]);
                }
            }

            //Act
            JArray jArray = information.serializeToJsonArray();
            string result = jArray.ToString();
            result = result.Replace(" ", "")
                    .Replace("\n", "")
                    .Replace("\r", "");

            //Assert
            Assert.Equal(json, result);
        }

        [Fact]
        public void SerializeToJsonObject_SerializeEmptyInformation_throwException()
        {
            //Arrange
            Information information = new Information();

            //Act & Assert
            var ex = Assert.Throws<MsgContainerNotSetException>(() => information.serializeToJsonArray());
            Assert.Equal("Information container doesn't have any settings", ex.Message);
        }


        public static IEnumerable<object[]> Data_DeserializeFromJsonArray_DeserializeInformation_ReturnCorrectJson() //TODO: add more tests
        {
            yield return new object[] { new List<object>() {new InformationObject(){ setting = EInformationSymbols.getFirmwareVersion, settingStatus = null, value = "1.15.243"}}, "[[\"firmVer\",\"1.15.243\"]]" };
        }
        [Theory]
        [MemberData(nameof(Data_DeserializeFromJsonArray_DeserializeInformation_ReturnCorrectJson))]
        public void DeserializeFromJsonArray_DeserializeInformation_ReturnCorrectJson(List<object> data, string json)
        {
            //Arrange
            Information information = new Information();
            JArray jArray = JArray.Parse(json);

            List<InformationObject> informationObjects = new List<InformationObject>();
            //rewrite data list to informationObjects list
            foreach (InformationObject item in data)
            {
                informationObjects.Add(item);
            }

            //act
            information.deserializeFromJsonArray(jArray);

            //assert
            Assert.Equal(informationObjects.ToString(), information.settings.ToString()); //convert lists to string for comparison
        }

        [Fact]
        public void DeserializeFromJsonArray_DeserializeIncorrectJson_throwException()
        {
            //Arrange
            Information information = new Information();
            JArray jArray = JArray.Parse("[[\"firmVer\"]]");

            //act & assert
            var ex = Assert.Throws<IncorrectMessageException>(() => information.deserializeFromJsonArray(jArray));

            Assert.Equal("Information message has missing key", ex.Message);
            Assert.Equal("[[\"firmVer\"]]", ex.Data["json"].ToString());
        }

        [Fact]
        public void DeserializeFromJsonArray_DeserializeJsonWithIncorrectInformationSymbol_throwException()
        {
            //Arrange
            Information information = new Information();
            JArray jArray = JArray.Parse("[[\"firmVer\",\"valueA\"],[\"incorrectSetting\",\"valueB\"]]");

            //act & assert
            var ex = Assert.Throws<IncorrectMessageException>(() => information.deserializeFromJsonArray(jArray));

            Assert.Equal("Information setting is incorrect", ex.Message);
            Assert.Equal("incorrectSetting", ex.InnerException.Data["value"]);
            Assert.Equal("[[\"firmVer\",\"valueA\"],[\"incorrectSetting\",\"valueB\"]]", ex.Data["json"]);
        }
    }
}
