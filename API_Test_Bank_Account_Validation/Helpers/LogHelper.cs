using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
 

namespace API_Test_Bank_Account_Validation.Helpers
{
    public static class LogHelper
    {
        //StringBuilder Object
        static StringBuilder sb = new StringBuilder();
        
        //Relative File Path
        public static string path = "..\\Custom_Logs\\";

        //Logging_function_To_Log_an_Info
        public static void LogInfo(string msg)
        {
            var tempMsg = DateTime.Now +"\n"+ " INFO: " + msg;
            sb.Append(tempMsg);
            //File.AppendAllText("C:/Users/ahmeeafa/source/repos/Bank_Account_BDD/Logs/" + "log.txt", sb.ToString());
            File.AppendAllText(path + "log.txt", sb.ToString());
            sb.Clear(); 
        }
  
        //Logging_function_To_Log_a_Request
        public static void LogRequest(RestRequest request)
        {
            var parameters = string.Empty;

            foreach (var item in request.Parameters)
            {
                parameters += string.Format("NAME:\t{0},\tVALUE:\t{1}\n", item.Name, item.Value);
            }

            //Capturing Date&Time, Request_Method, Request_URL, Request_Parameters
            var tempMsg = "\n\n**********API REQUEST DETAILS**********" +
                "\nDate & Time:\t"+ DateTime.Now + 
                "\nREQUEST METHOD:\t" + request.Method + 
                "\nREQUEST URL:\t\t" +request.Resource + 
                "\nREQUEST HEADERS:\n" + parameters;

            sb.Append(tempMsg);
            File.AppendAllText(path + "log.txt", sb.ToString());
            sb.Clear();
        }

        //Logging_function_To_Log_a_Response
        public static void LogResponse(RestResponse response)
        {
            //Capturing Date&Time, Response_Code, Response_Body, Response_Headers 
            var tempMsg = "\n\n**********API RESPONSE DETAILS**********" +
                "\nDate & Time:\t" + DateTime.Now + 
                "\nRESPONSE CODE:\t " + response.StatusCode + 
                "\nRESPONSE BODY:\t " + response.Content;
            sb.Append(tempMsg);
            File.AppendAllText(path + "log.txt", sb.ToString());
            sb.Clear();
        }
    } 
}

