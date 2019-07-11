using ApiGateway.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace ApiGateway.Controllers
{

    public class ApiUserRegisterController : ApiController
    {
        private paymentApiEntities db = new paymentApiEntities();
        Utility util = new Utility();

        /// <summary>
        /// to be able login
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        /// 
        // GET: api/ApiUserRegister     
        public DataMapUser GetApiPayment([FromUri]string Username, string Password)
        {

            var myUser = db.UserApiPayments.FirstOrDefault(u => u.username == Username && u.password == Password);

            if (myUser != null)
            {

                var CredentialSend = new DataMapUser
                {
                    Personal_Token = myUser.Personal_Token,
                    Username = myUser.username
                };


                return CredentialSend;
            }
            else
            {
                return null;
            }


        }


        /// <summary>
        /// to register the user and generate its personal token
        /// </summary>
        /// <param name="apiPayment"></param>
        /// <returns></returns>
        // POST: api/ApiUserRegister
        [ResponseType(typeof(DataMapUser))]
        public string PostApiPayment(DataMapUser apiPayment)
        {

            string PayKeyApi = util.PaymentGenerateKey();
            var UserReg = new UserApiPayment()
            {
                username = apiPayment.Username,
                password = apiPayment.Password,
                Personal_Token = PayKeyApi
            };

            db.UserApiPayments.Add(UserReg);
            db.SaveChanges();

            return "Registered";
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