Feature: Bank Account Validation
Validating Bank Account
 
@Positive_Case:A_User_with_Valid_Auth_key
Scenario: Validate_if_A_User_has_A_Valid_Auth_Key_And_AccountNo_Return_status_Code_OK
Given Set Bank Account 'API' Endpoint
When User Request With A " " Valid JWT Token Posted to API
Then API Returns Status OK with Valid "Q7DaxRnFls6IpwSW1SQ2FaTFOf7UdReAFNoKY68L"



@Negative_Case:_A_User_with_Invalid_Auth-Key
Scenario: Validate_if_A_User_with_Invalid_Auth_Key_And_Valid_Account_No_Return_status_Unauthorized
Given Set Bank Account 'API' Endpoint
When a Request with an Invalid Auth Key is posted to api
Then Api returns Authorization has been denied for this request 


@Negative_Case_With_incorrect_Bank_Account
Scenario: Validate_A_User_With_An_Invalid_AccountNo_Return_IsValid:False
Given Set Bank Account 'API' Endpoint
When A Request with an Invalid Bank Account is posted to API
Then An Api return IsValid_False 


 
@Case_With_An_Empty_Bank_Account
Scenario: Validate_The_Response_If_The_request_has_Empty_Bank_Account_No_And_Return_Request_Validation_Error
Given Set Bank Account 'API' Endpoint
When A Request with an Empty Bank Account is posted to API
Then An Api return Request Validation Error 

 
@Case_With_A_Bank_Account_Invalid_IBAN_Format_(Exceeding_Length_Limit)
Scenario: Validate_The_Bank_Account_With_An_Exceeding_Length_Limit
Given Set Bank Account 'API' Endpoint
When A Request with an Exceeding_Length_Limit Bank Account is posted to API
Then An Api return Format Validation Error 

  
@Case_With_A_Bank_Account_Invalid_IBAN_Format_(Short_Length)
Scenario: Validate_The_Bank_Account_With_Below_Minimum_Required_Limit
Given Set Bank Account 'API' Endpoint
When A Request with a Below Minimum Required Limit Bank Accountis posted to API
Then An Api return Short Length Format Validation Error 
 
 @Case_A_Bank_Account_Support_Multiple_Countries_IBAN_Formats
Scenario: Validation_Of_A_Bank_Account_Support_Multiple_Countries_IBAN_Formats
Given Set Bank Account 'API' Endpoint 
When A Request with a Different Countries IBAN  Posted to API
Then An Api Validation Return IBAN Format Response 

 

 @Case_With_A_Bank_Account_Every_Bug
Scenario: Validation_Of_A_Bank_Account_With_Multiple_Cases
Given Set Bank Account 'API' Endpoint 
When A Request with an "DK5000400440116243" Cases Posted to API
Then An Api return "IBAN" Validation Error 