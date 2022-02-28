using System;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using API_Test_Bank_Account_Validation.Models;
using API_Test_Bank_Account_Validation.Helpers;
using RestSharp;
using NUnit.Framework;

namespace API_Test_Bank_Account_Validation
{
    [Binding]
    public sealed class Bank_Account_Step_Definition
    {

        //Scenario_Context
        private readonly ScenarioContext _scenarioContext;

        public Bank_Account_Step_Definition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        /*API EndPoint*/
        [Given(@"Set Bank Account '(.*)' Endpoint")]
        public static string SetBankAccountAPIEndpoint(string url)
        {
            //API_END_POINTS
            url = "https://api-test.afterpay.dev/api/v3/validate/bank-account";
            return url;
        }



        /*User With Valid Auth-Key*/


        [When(@"User Request With A ""(.*)"" Valid JWT Token Posted to API")]
        public static Task<RestResponse> WhenUserRequestWithAValidJWTTokenPostedToAPI(string Auth_key)
        {
            var restClient = new RestClient();
            //API_Request_Type
            var request = new RestRequest(SetBankAccountAPIEndpoint("url"), Method.Post);

            //Request_Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Auth-Key", Auth_key);

            //Add Bank Account in Json Body
            request.AddJsonBody(new { bankAccount = "GB09HAOE91311808002317" });

            //Logging_Request_Content
            LogHelper.LogRequest(request);
            Console.WriteLine("**********API REQUEST DETAILS**********" +
                "\nDate & Time:\t" + DateTime.Now +
                "\nREQUEST METHOD:\t" + request.Method +
                "\nREQUEST URL:\t" + request.Resource);
            //Request Execution
            var response = restClient.ExecuteAsync(request);

            //Response Return
            return response;
        }
 
        [Then(@"API Returns Status OK with Valid ""(.*)""")]
        public async Task ThenAPIReturnsStatusOKWithValid(string Auth_Key)
        {
            var response = await WhenUserRequestWithAValidJWTTokenPostedToAPI(Auth_Key);

            //Validating API Response Code
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            //Logging_Response
            LogHelper.LogResponse(response);
            Console.WriteLine("**********API RESPONSE DETAILS**********" +
                "\nDate & Time:\t" + DateTime.Now +
                "\nRESPONSE CODE:\t " + response.StatusCode +
                "\nRESPONSE BODY:\t " + response.Content);

            //API_Response
            var Response = JsonConvert.DeserializeObject<Sample_Response_200>(response.Content);

            //Validating Response
            Assert.AreEqual(Response.isValid, true);
        }
         
        
        
        
                                                /*User Without Auth-Key*/
        [When(@"a Request with an Invalid Auth Key is posted to api")]
        public static Task<RestResponse> WhenARequestWithAnInvalidAuthKeyIsPostedToApi()
        {
            var restClient = new RestClient();

            //API_Request_Type
            var request = new RestRequest(SetBankAccountAPIEndpoint("url"), Method.Post);

            //Request_Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Auth-Key", "5343543534");

            //Add Bank Account in Json Body
            request.AddJsonBody(new { bankAccount = "GB09HAOE91311808002317" });

            //Logging_Request_Content
            LogHelper.LogRequest(request);
            Console.WriteLine("**********API REQUEST DETAILS**********" +
                "\nDate & Time:\t" + DateTime.Now +
                "\nREQUEST METHOD:\t" + request.Method +
                "\nREQUEST URL:\t" + request.Resource);
 
            //Request Execution
            var response = restClient.ExecuteAsync(request);

            //Response Return
            return response;
        }

        [Then(@"Api returns Authorization has been denied for this request")]
        public async Task ThenApiReturnsAuthorizationHasBeenDeniedForThisRequest()
        {
            var response = await WhenARequestWithAnInvalidAuthKeyIsPostedToApi();

            //Validating ResponseCode
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

            //Logging_Response
            LogHelper.LogResponse(response);
            Console.WriteLine("**********API RESPONSE DETAILS**********" +
    "\nDate & Time:\t" + DateTime.Now +
    "\nRESPONSE CODE:\t " + response.StatusCode +
    "\nRESPONSE BODY:\t " + response.Content);
            //API_Response
            var Response = JsonConvert.DeserializeObject<Sample_Response_401>(response.Content);

            //Validating_Response_Message
            Assert.AreEqual(Response.message, "Authorization has been denied for this request.");
        }




        /*User with an Invalid Bank Account Return isValid: False*/
        [When(@"A Request with an Invalid Bank Account is posted to API")]
        public static Task<RestResponse> WhenARequestWithAnInvalidBankAccountIsPostedToAPI()
        {
            var restClient = new RestClient();
            //API_Request_Type
            var request = new RestRequest(SetBankAccountAPIEndpoint("url"), Method.Post);

            //Request_Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L");

            //Add Bank Account in Json Body
            request.AddJsonBody(new { bankAccount = "INVALID-GB09HAOE91311808002317" });

            //Logging_Request_Content
            LogHelper.LogRequest(request);
            Console.WriteLine("**********API REQUEST DETAILS**********" +
               "\nDate & Time:\t" + DateTime.Now +
               "\nREQUEST METHOD:\t" + request.Method +
               "\nREQUEST URL:\t" + request.Resource);

            //Request Execution
            var response = restClient.ExecuteAsync(request);

            //Response Return
            return response;
        }


        [Then(@"An Api return IsValid_False")]
        public async Task ThenAnApiReturnIsValid_False()
        {
            var response = await WhenARequestWithAnInvalidBankAccountIsPostedToAPI();

            //Validating API Response Code
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            //Logging_Response
            LogHelper.LogResponse(response);
            Console.WriteLine("**********API RESPONSE DETAILS**********" +
    "\nDate & Time:\t" + DateTime.Now +
    "\nRESPONSE CODE:\t " + response.StatusCode +
    "\nRESPONSE BODY:\t " + response.Content);
            //API_Response
            var Response = JsonConvert.DeserializeObject<Sample_Response_200>(response.Content);

            //Validating Response
            Assert.AreEqual(Response.isValid, false);
        }



        /*User with an Empty Bank Account And Returns Request Validation Error*/

        [When(@"A Request with an Empty Bank Account is posted to API")]
        public static Task<RestResponse> WhenARequestWithAnEmptyBankAccountIsPostedToAPI()
        {
            var restClient = new RestClient();
            //API_Request_Type
            var request = new RestRequest(SetBankAccountAPIEndpoint("url"), Method.Post);

            //Request_Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L");

            //Empty Bank Account in Json Body
            request.AddJsonBody(new { bankAccount = " " });

            //Logging_Request_Content
            LogHelper.LogRequest(request);
            Console.WriteLine("**********API REQUEST DETAILS**********" +
               "\nDate & Time:\t" + DateTime.Now +
               "\nREQUEST METHOD:\t" + request.Method +
               "\nREQUEST URL:\t" + request.Resource);

            //Request Execution
            var response = restClient.ExecuteAsync(request);

            //Response Return
            return response;
        }

        [Then(@"An Api return Request Validation Error")]
        public async Task ThenAnApiReturnRequestValidationError()
        {
            var response = await WhenARequestWithAnEmptyBankAccountIsPostedToAPI();

            //Validating API Response Code
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

            //Logging_Response
            LogHelper.LogResponse(response);
            Console.WriteLine("**********API RESPONSE DETAILS**********" +
    "\nDate & Time:\t" + DateTime.Now +
    "\nRESPONSE CODE:\t " + response.StatusCode +
    "\nRESPONSE BODY:\t " + response.Content);

            //API Response
            var sampleResponse = JsonConvert.DeserializeObject<List<Risk_Check_Message>>(response.Content);

            var first = sampleResponse.First();
            var expectedCodes = new string[] { "400.002", "400.003", "400.009" };
            var expectedMessages = new string[]
                {
                "Value is required.",
                "Error converting value of type Salutation, possible values are: Mr, Mrs, Miss"
                };
            var expectedfieldReferences = new string[]
            {
                "bankAccount",
                "customer.salutation"
            };


            //Assert.That(first.type, Is.EqualTo(ErrorType.BusinessError));
            Assert.That(first.type, Is.TypeOf<ErrorType>());
            Assert.That(first.actionCode, Is.TypeOf<ActionCode>());
            Assert.That(first.code, Is.AnyOf(expectedCodes));
            Assert.That(first.message, Is.AnyOf(expectedMessages));
            Assert.That(first.fieldReference, Is.AnyOf(expectedfieldReferences));
        }




        /*User with an Invalid Bank Account (Exceeding Length Limit And Returns Error*/


        [When(@"A Request with an Exceeding_Length_Limit Bank Account is posted to API")]
        public static Task<RestResponse> WhenARequestWithAnExceeding_Length_LimitBankAccountIsPostedToAPI()
        {
            var restClient = new RestClient();
            //API_Request_Type
            var request = new RestRequest(SetBankAccountAPIEndpoint("url"), Method.Post);

            //Request_Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L");

            //Empty Bank Account in Json Body
            request.AddJsonBody(new { bankAccount = "702740374702740374702740374702740374" });

            //Logging_Request_Content
            LogHelper.LogRequest(request);
            Console.WriteLine("**********API REQUEST DETAILS**********" +
             "\nDate & Time:\t" + DateTime.Now +
             "\nREQUEST METHOD:\t" + request.Method +
             "\nREQUEST URL:\t" + request.Resource);

            //Request Execution
            var response = restClient.ExecuteAsync(request);

            //Response Return
            return response;
        }

        [Then(@"An Api return Format Validation Error")]
        public async Task ThenAnApiReturnFormatValidationError()
        {
            var response = await WhenARequestWithAnExceeding_Length_LimitBankAccountIsPostedToAPI();

            //Validating API Response Code
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

            //Logging_Response
            LogHelper.LogResponse(response);
            Console.WriteLine("**********API RESPONSE DETAILS**********" +
    "\nDate & Time:\t" + DateTime.Now +
    "\nRESPONSE CODE:\t " + response.StatusCode +
    "\nRESPONSE BODY:\t " + response.Content);

            //API Response
            var sampleResponse = JsonConvert.DeserializeObject<List<Risk_Check_Message>>(response.Content);

            var first = sampleResponse.First();

            //Asserting_Response
            Assert.That(first.type, Is.EqualTo(ErrorType.BusinessError));
            Assert.That(first.actionCode, Is.EqualTo(ActionCode.AskConsumerToReEnterData));
            Assert.That(first.code, Is.EqualTo("400.005"));
            Assert.That(first.message, Is.EqualTo("A string value exceeds maximum length of 34."));
            Assert.That(first.fieldReference, Is.EqualTo("bankAccount"));
        }




        /*User with an Invalid Bank Account with Short Length And Returns Error*/

        [When(@"A Request with a Below Minimum Required Limit Bank Accountis posted to API")]
        public static Task<RestResponse> WhenARequestWithABelowMinimumRequiredLimitBankAccountisPostedToAPI()
        {
            var restClient = new RestClient();
            //API_Request_Type
            var request = new RestRequest(SetBankAccountAPIEndpoint("url"), Method.Post);

            //Request_Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L");

            //Empty Bank Account in Json Body
            request.AddJsonBody(new { bankAccount = "2121" });


            //Logging_Request_Content
            LogHelper.LogRequest(request);
            Console.WriteLine("**********API REQUEST DETAILS**********" +
                "\nDate & Time:\t" + DateTime.Now +
                "\nREQUEST METHOD:\t" + request.Method +
                "\nREQUEST URL:\t" + request.Resource);

            //Request Execution
            var response = restClient.ExecuteAsync(request);

            //Response Return
            return response;
        }

        [Then(@"An Api return Short Length Format Validation Error")]
        public async Task ThenAnApiReturnShortLengthFormatValidationError()
        {
            var response = await WhenARequestWithABelowMinimumRequiredLimitBankAccountisPostedToAPI();

            //Validating API Response Code
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

            //Logging_Response
            LogHelper.LogResponse(response);
            Console.WriteLine("**********API RESPONSE DETAILS**********" +
    "\nDate & Time:\t" + DateTime.Now +
    "\nRESPONSE CODE:\t " + response.StatusCode +
    "\nRESPONSE BODY:\t " + response.Content);
            //API Response
            var sampleResponse = JsonConvert.DeserializeObject<List<Risk_Check_Message>>(response.Content);

            var first = sampleResponse.First();

            Assert.That(first.type, Is.EqualTo(ErrorType.BusinessError));
            Assert.That(first.actionCode, Is.EqualTo(ActionCode.AskConsumerToReEnterData));
            Assert.That(first.code, Is.EqualTo("400.006"));
            Assert.That(first.message, Is.EqualTo("A string value with minimum length 7 is required."));
            Assert.That(first.fieldReference, Is.EqualTo("bankAccount"));
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                                                            /*Case_A_Bank_Account_Support_Multiple_Countries_IBAN_Formats*/

        [When(@"A Request with a Different Countries IBAN  Posted to API")]
        public static Task<RestResponse> WhenARequestWithADifferentCountriesIBANPostedToAPI()
        {
            var restClient = new RestClient();
            //API_Request_Type
            var request = new RestRequest(SetBankAccountAPIEndpoint("url"), Method.Post);

            //Request_Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L");

            //SE IBAN in Json Body (isValid=true) Case
            request.AddJsonBody(new { bankAccount = "SE4550000000058398257466" });

            //IsValid=False Case
            //request.AddJsonBody(new { bankAccount = "SE8796528000010123456789" });


            //Logging_Request_Content
            LogHelper.LogRequest(request);
            Console.WriteLine("**********API REQUEST DETAILS**********" +
                    "\nDate & Time:\t" + DateTime.Now +
                    "\nREQUEST METHOD:\t" + request.Method +
                    "\nREQUEST URL:\t" + request.Resource);

            //Request Execution
            var response = restClient.ExecuteAsync(request);

            //Response Return
            return response;
        }
 
        [Then(@"An Api Validation Return IBAN Format Response")]
        public async Task ThenAnApiValidationReturnIBANFormatResponse()
        {
            var response = await WhenARequestWithADifferentCountriesIBANPostedToAPI();

            //Validating API Response Code
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            //Logging_Response
            LogHelper.LogResponse(response);
            Console.WriteLine("**********API RESPONSE DETAILS**********" +
    "\nDate & Time:\t" + DateTime.Now +
    "\nRESPONSE CODE:\t " + response.StatusCode +
    "\nRESPONSE BODY:\t " + response.Content);

            //Deserialization of a response content
            var APIResponse = JsonConvert.DeserializeObject<Sample_Response_200>(response.Content);

            //Validating API Response
            if (APIResponse.isValid == true)
            {
                //Logging_Response
                LogHelper.LogResponse(response);

                //Asserting isValid=true
                Assert.AreEqual(APIResponse.isValid, true);
            }

            if (APIResponse.isValid == false)
            {
                //Logging_Response
                LogHelper.LogResponse(response);

                //Assert API Repsonse 
                var first = APIResponse.riskCheckMessages.First();
                Assert.That(first.type, Is.EqualTo(ErrorType.BusinessError));
                Assert.That(first.actionCode, Is.EqualTo(ActionCode.AskConsumerToReEnterData));
                Assert.That(first.code, Is.EqualTo("200.908"));
                Assert.That(first.message, Is.EqualTo("Value format is incorrect."));
                Assert.That(first.customerFacingMessage, Is.EqualTo("Die eingegebenen Bankdaten sind ungültig, bitte überprüfen und korrigieren."));
                Assert.That(first.fieldReference, Is.EqualTo("bankAccount"));
            }
        }



                                                            /*Case_Cater_different_scenarios_For_A_Bank_Account_Validation*/

        [When(@"A Request with an ""(.*)"" Cases Posted to API")]
        public static Task<RestResponse> WhenARequestWithAnIBANCasesPostedToAPI(string BankAccount)
        {
            var restClient = new RestClient();
            
            //API_Request_Type
            var request = new RestRequest(SetBankAccountAPIEndpoint("url"), Method.Post);

            //Request_Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Auth-Key", "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L");

            //Bank Account in Json Body 
            request.AddJsonBody(new { bankAccount = BankAccount });

            //Logging_Request_Content
            LogHelper.LogRequest(request);
            Console.WriteLine("**********API REQUEST DETAILS**********" +
                  "\nDate & Time:\t" + DateTime.Now +
                  "\nREQUEST METHOD:\t" + request.Method +
                  "\nREQUEST URL:\t" + request.Resource);

            //Request Execution
            var response = restClient.ExecuteAsync(request);

            //Response Return
            return response;
        }

        [Then(@"An Api return ""(.*)"" Validation Error")]
        public async Task ThenAnApiValidationReturnsIBANFormatResponse(string BankAcc)
        {
            var response = await WhenARequestWithAnIBANCasesPostedToAPI(BankAcc);

            var expectedMessages = new string[]
                   {
                    "Value is required.",
                    "Value format is incorrect.",
                    "Error converting value of type Salutation, possible values are: Mr, Mrs, Miss",
                    "A string value exceeds maximum length of 34.",
                    "A string value with minimum length 7 is required.",
                    "The body of the request is missing, or its format is invalid."
                   };

            var expectedfieldReferences = new string[]
                {
                 "bankAccount",
                  "customer.salutation",
                  " "
                };
            var expectedCodes = new string[] { "400.001", "400.002", "400.003", "400.005", "400.006", "400.009", "200.908" };
            var expectedCustomerfacingmessage = new string[]
            {
                    "Value is required.",
                    "Error converting value of type Salutation, possible values are: Mr, Mrs, Miss",
                    "A string value exceeds maximum length of 34.",
                    "A string value with minimum length 7 is required.",
                    "The body of the request is missing, or its format is invalid.",
                    "Die eingegebenen Bankdaten sind ungültig, bitte überprüfen und korrigieren."
            };


            //Validating API Response Code
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                //Re-Validating ResponseCode
                Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);

                //Logging Response
                LogHelper.LogResponse(response);
                Console.WriteLine("**********API RESPONSE DETAILS**********" +
    "\nDate & Time:\t" + DateTime.Now +
    "\nRESPONSE CODE:\t " + response.StatusCode +
    "\nRESPONSE BODY:\t " + response.Content);


                //API Response_Content
                var sampleResponse = JsonConvert.DeserializeObject<List<Risk_Check_Message>>(response.Content);
                var first = sampleResponse.First();

                if ((first.actionCode == ActionCode.AskConsumerToReEnterData) && first.type == ErrorType.BusinessError)
                {
                    if (first.message == "A string value with minimum length 7 is required." && first.code == "400.006")
                    {
                        Assert.That(first.type, Is.TypeOf<ErrorType>());
                        Assert.That(first.actionCode, Is.TypeOf<ActionCode>());
                        Assert.That(first.code, Is.AnyOf(expectedCodes));
                        Assert.That(first.message, Is.AnyOf(expectedMessages));
                        Assert.That(first.customerFacingMessage, Is.AnyOf(expectedCustomerfacingmessage));
                        Assert.That(first.fieldReference, Is.AnyOf(expectedfieldReferences));
                    }

                    if (first.message == "A string value exceeds maximum length of 34." && first.code == "400.005")
                    {
                        Assert.That(first.type, Is.TypeOf<ErrorType>());
                        Assert.That(first.actionCode, Is.TypeOf<ActionCode>());
                        Assert.That(first.code, Is.AnyOf(expectedCodes));
                        Assert.That(first.message, Is.AnyOf(expectedMessages));
                        Assert.That(first.customerFacingMessage, Is.AnyOf(expectedCustomerfacingmessage));
                        Assert.That(first.fieldReference, Is.AnyOf(expectedfieldReferences));
                    }

                    if (first.message == "Value is required." && first.code == "400.002")
                    {
                        Assert.That(first.type, Is.TypeOf<ErrorType>());
                        Assert.That(first.actionCode, Is.TypeOf<ActionCode>());
                        Assert.That(first.code, Is.AnyOf(expectedCodes));
                        Assert.That(first.message, Is.AnyOf(expectedMessages));
                        Assert.That(first.customerFacingMessage, Is.AnyOf(expectedCustomerfacingmessage));
                        Assert.That(first.fieldReference, Is.AnyOf(expectedfieldReferences));
                    }
                }
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Re-Validating ResponseCode
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                LogHelper.LogResponse(response);

                //API Response_Content
                var sampleResponse = JsonConvert.DeserializeObject<Sample_Response_200>(response.Content);
                var first = sampleResponse.riskCheckMessages.First();

                if (sampleResponse.isValid == true)
                {
                    //Validating Response
                    Assert.AreEqual(sampleResponse.isValid, true);
                }

                if (sampleResponse.isValid == false && first.actionCode == ActionCode.AskConsumerToReEnterData && first.type == ErrorType.BusinessError && first.message == "Value format is incorrect." && first.code == "200.908")
                {
                    Assert.That(first.type, Is.TypeOf<ErrorType>());
                    Assert.That(first.actionCode, Is.TypeOf<ActionCode>());
                    Assert.That(first.code, Is.AnyOf(expectedCodes));
                    Assert.That(first.message, Is.AnyOf(expectedMessages));
                    Assert.That(first.customerFacingMessage, Is.AnyOf(expectedCustomerfacingmessage));
                    Assert.That(first.fieldReference, Is.AnyOf(expectedfieldReferences));
                }
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //Validating ResponseCode
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized);

                //Logging_Response
                LogHelper.LogResponse(response);

                //API Response_Content
                var Response = JsonConvert.DeserializeObject<Sample_Response_401>(response.Content);

                //Validating_Response_Message
                Assert.AreEqual(Response.message, "Authorization has been denied for this request.");
            }

        }
  
    }
}
 