using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bankside.Models
{
    public class CardCheck
    {
        public string firstname { get; set; }
        public string surname { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string card_Type { get; set; }
        public string Payment_Token { get; set; }
        public string currency { get; set; }
        public string Card_Number { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Card_Expiration { get; set; }
        public Nullable<int> Card_cvv { get; set; }
    }

    public class ApprovalResponse
    {
       
        public string Payment_Token { get; set; }
        public bool approval_response { get; set; }
       
    }
}