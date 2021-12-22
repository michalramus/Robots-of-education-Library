using System;
using Xunit;
using ROELibrary;
using Newtonsoft.Json.Linq;

namespace UnitTests.Message
{
    public class ErrorTests
    {
        [Theory]
        [InlineData("{\"errorType\":\"incorrectPinout\",\"errMsg\":\"message\",\"errValue\":\"value123\",\"firmVer\":\"1.18.5\"}", (int)EErrorSymbols.incorrectPinout, "message", "value123", "1.18.5")]
        [InlineData("{\"errorType\":\"incorrectPinout\",\"errMsg\":\"wrong pinout 885\",\"errValue\":\"pin 4 - wrong\",\"firmVer\":\"0.2 alpha\"}", (int)EErrorSymbols.incorrectPinout, "wrong pinout 885", "pin 4 - wrong", "0.2 alpha")]
        [InlineData("{\"errorType\":\"incorrectPinout\",\"errMsg\":\"TestTest\",\"errValue\":\"testValue\",\"firmVer\":\"gdd firmware\"}", (int)EErrorSymbols.incorrectPinout, "TestTest", "testValue", "gdd firmware")]
        [InlineData("{\"errorType\":\"incorrectPinout\",\"errMsg\":\"Human Avon Applications\",\"errValue\":\"val-incorrect pin 1500100700\",\"firmVer\":\"15.14.13\"}", (int)EErrorSymbols.incorrectPinout, "Human Avon Applications", "val-incorrect pin 1500100700", "15.14.13")]
        [InlineData("{\"errorType\":\"incorrectPinout\",\"errMsg\":\"wrong   number\",\"errValue\":\"f3d012ad-96d7-4fc6-b65e-c9c3f31cff51\",\"firmVer\":\"95387.79\"}", (int)EErrorSymbols.incorrectPinout, "wrong   number", "f3d012ad-96d7-4fc6-b65e-c9c3f31cff51", "95387.79")]
        public void deserializeFromJsonObject_deserializeCorrectObject_returnObject(string json, int errorType, string message, string value, string firmwareVersion)
        {
            //arrange
            Error error = new Error();
            JObject jsonObject = JObject.Parse(json);

            //act
            error.deserializeFromJsonObject(jsonObject);

            //assert
            Assert.Equal((EErrorSymbols)errorType, error.errorType);
            Assert.Equal(message, error.message);
            Assert.Equal(value, error.value);
            Assert.Equal(firmwareVersion, error.firmwareVersion);
        }

        [Fact]
        public void deserializeFromJsonObject_missingErrorType_throwException()
        {
            //arrange
            Error error = new Error();
            JObject jsonObject = JObject.Parse("{\"errMsg\":\"messageAAbbCC\"}");

            //act & assert
            IncorrectMessageException ex = Assert.Throws<IncorrectMessageException>(() => error.deserializeFromJsonObject(jsonObject));

            string errorJson = ex.Data["json"].ToString();
            errorJson = errorJson.Replace("\n", "").Replace(" ", "");

            Assert.Equal("Error message has missing key", ex.Message);
            Assert.Equal("{\"errMsg\":\"messageAAbbCC\"}", errorJson);
        }
    }
}
