using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Bankside.Models;
using Newtonsoft.Json;

namespace Bankside.Controllers
{
    public class BankTransactionsController : ApiController
    {
        private BankEntities db = new BankEntities();
       private CardValidation crd = new CardValidation();
        // GET: api/BankTransactions
     
        // GET: api/BankTransactions/5
        [ResponseType(typeof(BankTransaction))]
        public IHttpActionResult GetBankTransaction(int id)
        {
            BankTransaction bankTransaction = db.BankTransactions.Find(id);
            if (bankTransaction == null)
            {
                return NotFound();
            }

            return Ok(bankTransaction);
        }

       
        /// <summary>
        /// Send transaction to be processed
        /// </summary>
        /// <param name="bankTransaction"></param>
        /// <returns></returns>
        /// 
        // POST: api/BankTransactions
        [ResponseType(typeof(CardCheck))]
        public ApprovalResponse PostBankTransaction(CardCheck bankTransaction)
        {
            int cardCVV = Convert.ToInt32(bankTransaction.Card_cvv);
            var CashPay = Convert.ToDecimal(bankTransaction.Amount);
            string response =  crd.CardValidationAccount(CashPay, cardCVV, bankTransaction.Card_Number, bankTransaction.Card_Expiration, bankTransaction.card_Type,bankTransaction.firstname,bankTransaction.surname);
            
            if (response == "Successful")
            {
                var transaction = new ApprovalResponse
                {
                    approval_response = true,
                    Payment_Token = bankTransaction.Payment_Token

                };
               

                return transaction;
            }
            else
            {
                return null;
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

        private bool BankTransactionExists(int id)
        {
            return db.BankTransactions.Count(e => e.Id == id) > 0;
        }
    }
}