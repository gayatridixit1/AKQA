using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using AKQAGayatri.Interface;
using AKQAGayatri.Models;

namespace AKQAGayatri.Controllers
{
    public class ConversionController : ApiController
    {

        public JsonResult GetConvertNumtoAlpha(Input personDetails)
        {
            if (personDetails != null && !string.IsNullOrEmpty(personDetails?.Name) && personDetails?.Amount != null)
            {
                var model = ConvertAmountToAplha(personDetails);
                return new JsonResult()
                {
                    Data = new { outPut = model, StatusCode = 200 },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult()
            {
                Data = new { outPut = "", StatusCode = 400 },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public string ConvertNumbertoWords(long number)
        {
            string alphabeticVal = string.Empty;
            
            alphabeticVal += NumberToWords((number / 1000000), "Million ");
            
            alphabeticVal += NumberToWords(((number / 1000) % 100), "thousand ");
            
            alphabeticVal += NumberToWords(((number / 100) % 10), "hundred ");



            if (number > 100 && number % 100 > 0)
                alphabeticVal += "and ";
            
            alphabeticVal += NumberToWords((number % 100), "");

            return alphabeticVal;
        }

        public Input ConvertAmountToAplha(Input input)
        {
            Input details = new Input();
            try
            {
                string amount = input?.Amount;
                string wholeNumber = string.Empty, decimalNumber = string.Empty;
                if (string.IsNullOrEmpty(amount))
                {
                    return details;
                }
                int decimalPlace = 0;
                if (input.Amount.Contains("."))
                {

                    amount = String.Format("{0:0.00}", decimal.Parse(amount));
                    decimalPlace = input.Amount.IndexOf(".");
                    if (decimalPlace == 0)
                    {
                        decimalNumber = amount.Substring(decimalPlace + 2);
                    }
                    else
                    {
                        wholeNumber = decimalPlace > 0 ?
                                            amount.Substring(0, decimalPlace) : amount;
                        decimalNumber = amount.Substring(decimalPlace + 1);
                    }
                }
                else
                {
                    wholeNumber = amount;
                }
                long wholeLongNumber = 0;
                long decimalLongNumber = 0;

                if (!string.IsNullOrEmpty(wholeNumber))
                {
                    long.TryParse(wholeNumber, out wholeLongNumber);
                }
                string dollarOutPut = string.Empty;
                dollarOutPut = ConvertNumbertoWords(wholeLongNumber);
                dollarOutPut = !string.IsNullOrEmpty(dollarOutPut) ? dollarOutPut + " dollar" : string.Empty;

                string centOutPut = string.Empty;

                if (!string.IsNullOrEmpty(decimalNumber))
                {
                    string andString = string.Empty;
                    andString = !string.IsNullOrEmpty(dollarOutPut) ? " and " : "";
                    long.TryParse(decimalNumber, out decimalLongNumber);
                    centOutPut = ConvertNumbertoWords(decimalLongNumber);
                    centOutPut = !string.IsNullOrEmpty(centOutPut) ? andString + centOutPut + " Cents" : string.Empty;
                }
                string outPut = string.Concat(dollarOutPut, centOutPut);
                details.Name = input.Name;
                details.Amount = outPut;
                return details;
            }
            catch (Exception ex)
            {
                return details;
            }
        }

        private string NumberToWords(long number, string inputString)
        {
            var ones = new[]
            {
                "",
                "one ",
                "two ",
                "three ",
                "four ",
                "five ",
                "six ",
                "seven ",
                "eight ",
                "nine ",
                "ten ",
                "eleven ",
                "twelve ",
                "thirteen ",
                "fourteen ",
                "fifteen ",
                "sixteen ",
                "seventeen ",
                "eighteen ",
                "nineteen "
            };
            
            var tens = new[]
            {
                "",
                "",
                "twenty ",
                "thirty ",
                "forty ",
                "fifty ",
                "sixty ",
                "seventy ",
                "eighty ",
                "ninety "
               };


            string outputString = string.Empty;
            // if n is more than 19ss
            if (number > 19)
                outputString += tens[number / 10] + ones[number % 10];
            else
                outputString += ones[number];

            // if n is not zero
            if (number != 0)
                outputString += inputString;

            return outputString;
        }
    }
}

