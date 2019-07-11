using System;
using System.Linq;

namespace Bankside.Models
{
    public class CardValidation
    {
        private BankEntities db = new BankEntities();


        /// <summary>
        /// Validation to check if the card is valid
        /// </summary>
        /// <param name="valueTransaction"></param>
        /// <param name="CVVValue"></param>
        /// <param name="valueCardN"></param>
        /// <param name="valueExpiry"></param>
        /// <param name="valueType"></param>
        /// <param name="valuefirst"></param>
        /// <param name="valuesur"></param>
        /// <returns></returns>
        public string CardValidationAccount(decimal valueTransaction, int CVVValue, string valueCardN, string valueExpiry, string valueType, string valuefirst, string valuesur)
        {

            if (CardCredential(CVVValue, valueCardN, valueType, valuefirst, valuesur))
            {
                if (CardCredentialExpiration(valueExpiry))
                {
                    if (CardCredentialBalance(CVVValue, valueCardN, valueTransaction))
                    {
                        return "Successful";
                    }
                    else
                    {
                        return "Insufficient Balance";
                    }

                }
                else
                {
                    return "Card Expired";
                }

            }
            else
            {
                return "Wrong Credential";
            }

        }

        /// <summary>
        /// This check the card credential
        /// </summary>
        /// <param name="CVVValue"></param>
        /// <param name="valueCardN"></param>
        /// <param name="valueType"></param>
        /// <param name="valuefirst"></param>
        /// <param name="valuesur"></param>
        /// <returns></returns>
        private bool CardCredential(int CVVValue, string valueCardN, string valueType, string valuefirst, string valuesur)
        {
            var CredentialCard = db.BankTransactions.FirstOrDefault(u => u.Card_cvv == CVVValue && u.Card_Number == valueCardN &&
            u.card_Type == valueType && u.firstname == valuefirst && u.surname == valuesur);
            if (CredentialCard != null)
            {

                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// To check if the card is expired 
        /// </summary>
        /// <param name="valueExpiry"></param>
        /// <returns></returns>
        private bool CardCredentialExpiration(string valueExpiry)
        {
            int CurrentYear = Convert.ToInt32(DateTime.Now.Year.ToString());
            DateTime now = DateTime.Today;
            int currentMonth = Convert.ToInt32(now.ToString("MM"));
            int Cardmonth = Convert.ToInt32(valueExpiry.Substring(0, 2));
            int cardyear = Convert.ToInt32(valueExpiry.Substring(3, 4));

            if (currentMonth > Cardmonth && CurrentYear > cardyear)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// To process the payment amount to credit balance card 
        /// </summary>
        /// <param name="CVVValue"></param>
        /// <param name="valueCardN"></param>
        /// <param name="valueTransaction"></param>
        /// <returns></returns>
        private bool CardCredentialBalance(int CVVValue, string valueCardN, decimal valueTransaction)
        {
            var CredentialCard = db.BankTransactions.FirstOrDefault(u => u.Card_cvv == CVVValue && u.Card_Number == valueCardN);
            double cardTransact = decimal.ToDouble(valueTransaction);
            double BalanceValue = Convert.ToDouble(CredentialCard.Total_Balance);
            if (cardTransact >= BalanceValue)
            {

                return false;
            }
            else
            { 
                //update the balance
                double NewBalance = BalanceValue - cardTransact;
                CredentialCard.Total_Balance = Convert.ToDecimal(NewBalance);
                db.SaveChanges();
                return true;
            }

        }

        /// <summary>
        /// to return the balance on the interface 
        /// </summary>
        /// <param name="CVVValue"></param>
        /// <param name="valueCardN"></param>
        /// <returns></returns>
        public string CheckBalance(int CVVValue, string valueCardN)
        {
            var BalanceTotal = db.BankTransactions.FirstOrDefault(u => u.Card_cvv == CVVValue && u.Card_Number == valueCardN);
           
            if(BalanceTotal != null)
            {
                string BalanceTotalCash = Convert.ToString(BalanceTotal.Total_Balance);
                return BalanceTotalCash;
            }
            else
            {
                return null;
            }
        }
    }
}