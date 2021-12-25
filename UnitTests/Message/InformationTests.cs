using Xunit;
using ROELibrary;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace UnitTests.Message
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
                    .Replace("\n", "");

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
    }
}
