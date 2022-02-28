using System;
using System.Collections.Generic;
using System.Text;
using API_Test_Bank_Account_Validation.Helpers;
using API_Test_Bank_Account_Validation.Models;

namespace API_Test_Bank_Account_Validation.Models
{

    class Sample_Response_200
    {
        public bool isValid { get; set; }
        public List<Risk_Check_Message> riskCheckMessages { get; set; }
    }
}
 