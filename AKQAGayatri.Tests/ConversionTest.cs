using System;
using System.Linq;
using System.Web.Mvc;
using AKQAGayatri.Controllers;
using AKQAGayatri.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AKQAGayatri.Tests
{
    [TestClass]
    public class Conversion
    {
        [TestMethod]
        public void ConversionTestMethod()
        {
            Conversion conversionObj = new Conversion();
            Input inputDetails = new Input();
            inputDetails.Name = "Gayatri";
            inputDetails.Amount = "457";

            Input personDetailsMock = new Input() { Name = "Gayatri", Amount = "four hundred and fifty seven  dollar"};
            ConversionController conversionControllerObj = new ConversionController();

            var result = conversionControllerObj.GetConvertNumtoAlpha(inputDetails) as JsonResult;

            Assert.AreEqual(200, GetValue<int>(result, "StatusCode"));
            Assert.AreEqual("Gayatri", GetValue<Input>(result, "outPut").Name);
            Assert.AreEqual("four hundred and fifty seven  dollar", GetValue<Input>(result, "outPut").Amount);
        }

        [TestMethod]
        public void ShouldReturnDecimal()
        {
            Conversion conversionObj = new Conversion();
            Input inputDetails = new Input();
            inputDetails.Name = "Gayatri";
            inputDetails.Amount = "456.45";

            Input personDetailsMock = new Input() { Name = "Gayatri", Amount = "four hundred and fifty six  dollar and forty five  Cents" };
            
            ConversionController conversionControllerObj = new ConversionController();

            var result = conversionControllerObj.GetConvertNumtoAlpha(inputDetails) as JsonResult;

            Assert.AreEqual(200, GetValue<int>(result, "StatusCode"));
            Assert.AreEqual("Gayatri", GetValue<Input>(result, "outPut").Name);
            Assert.AreEqual("four hundred and fifty six  dollar and forty five  Cents", GetValue<Input>(result, "outPut").Amount);
        }

        private T GetValue<T>(JsonResult jsonResult, string propertyName)
        {
            var property = jsonResult.Data.GetType().GetProperties()
                    .Where(p => string.Compare(p.Name, propertyName) == 0)
                    .FirstOrDefault();
            if (null == property)
                throw new ArgumentException("propertyName not found", "propertyName");
            return (T)property.GetValue(jsonResult.Data, null);
        }
    }
}
