using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using API_Test_Bank_Account_Validation.Helpers;
using API_Test_Bank_Account_Validation.Models;


namespace API_Test_Bank_Account_Validation.Models
{
    class Risk_Check_Message
    { 
 
        public ActionCode actionCode { get; set; }
        public string code { get; set; }
        public string fieldReference { get; set; }
        public string message { get; set; }
        public string customerFacingMessage { get; set; }
        public ErrorType type { get; set; }
         
    }

    enum ActionCode
    {
        Unavailable,
        AskConsumerToConfirm,
        AskConsumerToReEnterData,
        OfferSecurePaymentMethods,
        RequiresSsn
    }

    enum ErrorType
    {
        BusinessError,
        TechnicalError,
        NotificationMessage
    }

}
