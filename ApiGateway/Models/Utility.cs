using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;


namespace ApiGateway.Models
{
    public class Utility
    {

        paymentApiEntities apiEntities = new paymentApiEntities();
      

        /// <summary>
        /// // It allow to generate for Personal Token and Payment Token
        /// </summary>
        /// <returns></returns>
        public string PaymentGenerateKey()
        {
            int size = 20;
            string output = string.Empty;
            while (true)
            {
                output = output + Path.GetRandomFileName().Replace(".", string.Empty);
                if (output.Length > size)
                {
                    output = output.Substring(0, size);
                    break;
                }
            }
            return output;
        }

        /// <summary>
        /// This Method is for processing the transaction to bank side
        /// </summary>
        /// <param name="ClientMap"></param>
        /// <param name="cashpay"></param>
        /// <param name="cardCVV"></param>
        /// <param name="PayKeyApi"></param>
        /// <returns></returns>
        public bool UpdateTransaction(DataMap ClientMap, decimal cashpay, int cardCVV, string PayKeyApi)
        {
            WebClient bankclient = new WebClient();


            var data = new
            {
                firstname = ClientMap.Firstname,
                surname = ClientMap.Surname,
                address = ClientMap.Address,
                email = ClientMap.Email,
                card_Type = ClientMap.CardType,
                Payment_Token = PayKeyApi,
                currency = ClientMap.Currency,
                Card_Number = ClientMap.Card_Number,
                Amount = cashpay,
                Card_Expiration = ClientMap.Card_Expiration,
                Card_cvv = cardCVV
            };


            var dataString = JsonConvert.SerializeObject(data);
            bankclient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string response = bankclient.UploadString(new Uri("https://localhost:44338/api/BankTransactions"), "POST", dataString);
            var listaURL = JsonConvert.DeserializeObject<BankApprovalResponse>(response);
            string BankResponseToken = listaURL.Payment_Token;// return token from bank


            // this will update the approval status after receiving  the confirmation of transaction from the bank side
            if (BankResponseToken != null)
            {
                using (paymentApiEntities dbx = new paymentApiEntities())
                {
                    var result = dbx.ApiPayments.SingleOrDefault(b => b.Payment_Token == BankResponseToken);
                    result.Payment_Approval = true;
                    dbx.SaveChanges();


                    if (result != null)
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// this check the personal token if it exists
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool TokenChecker(string param)
        {
            try
            {
                var order = apiEntities.ApiPayments.FirstOrDefault(x => x.Personal_Token == param);
                if (order != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception er)
            {
                return false;
            }

        }
    }
}