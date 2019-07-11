using ApiGateway.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApiGateway.Controllers
{
    public class ApiPaymentsController : ApiController
    {
        private paymentApiEntities db = new paymentApiEntities();
        private Utility util = new Utility();



        /// <summary>
        /// to get previous transaction
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
       
        // GET: api/ApiPayments?token = {}
        [ResponseType(typeof(ApiPayment))]
        public IHttpActionResult GetApiPayment([FromUri]string token)
        {
            if (util.TokenChecker(token))
            {
                var myUser = db.ApiPayments.Where(u => u.Personal_Token == token);
                return Ok(myUser);
            }
            else
            {
                return NotFound();
            }
        }



        /// <summary>
        /// to post the transaction
        /// </summary>
        /// <param name="ClientMap"></param>
        /// <returns></returns>

        // POST: api/ApiPayments    
        public string PostApiPayment(DataMap ClientMap)
        {

            try
            {

                string PayKeyApi = util.PaymentGenerateKey();
                DateTime PayDay = DateTime.Now;
                int cardCVV = Convert.ToInt32(ClientMap.Card_cvv);
                var CashPay = Convert.ToDecimal(ClientMap.Amount);
                var PayEntry = new ApiPayment()
                {
                    firstname = ClientMap.Firstname,
                    surname = ClientMap.Surname,
                    address = ClientMap.Address,
                    email = ClientMap.Email,
                    card_Type = ClientMap.CardType,
                    currency = ClientMap.Currency,
                    Personal_Token = ClientMap.Personal_Token,
                    Payment_Token = PayKeyApi,
                    Card_Number = ClientMap.Card_Number,
                    Amount = CashPay,
                    Card_Expiration = ClientMap.Card_Expiration,
                    Card_cvv = cardCVV,
                    Payment_Approval = false,
                    Payment_date = PayDay,
                };
                db.ApiPayments.Add(PayEntry);
                db.SaveChanges();
                if (util.UpdateTransaction(ClientMap, CashPay, cardCVV, PayKeyApi))
                {
                    return "Successful";
                }
                else
                {
                    return "Unsuccessful";
                }
            }
            catch (Exception er)
            {
                return er.ToString();
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApiPaymentExists(int id)
        {
            return db.ApiPayments.Count(e => e.Id == id) > 0;
        }
    }
}