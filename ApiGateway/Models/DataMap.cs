using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiGateway.Models
{
    // model to post a transaction from the client to api
    public class DataMap
    {
    
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string CardType { get; set; }
        public string Currency { get; set; }
        public string Personal_Token { get; set; }
        public string Card_Number { get; set; }
        public string Amount { get; set; }
        public string Card_Expiration { get; set; }
        public string Card_cvv { get; set; }

    }

    // response models for login
    public class DataMapUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Personal_Token { get; set; }

    }

    // response model class from bank
    public class BankApprovalResponse
    {
        public string Payment_Token { get; set; }
        public bool approval_response { get; set; }
    }
}