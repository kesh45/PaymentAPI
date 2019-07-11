using ClientSide.Models;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace ClientSide.Controllers
{
    public class HomeController : Controller
    {
       private Autho authen = new Autho();
       private  datavalid valid = new datavalid();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buy1()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Buyer2()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// To able to register to the API
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Authentication_Reg(string param1, string param2)
        {
                
            if (authen.ApiRegistration(param1, param2))
            {                
                string Rs = "Registered";
                return Json(Rs);
            }
            else
            {
                string error = "Cant Registered";
                return Json(error);
            }
        }


        /// <summary>
        /// TO be able to login
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public JsonResult Authentication_Log(string param1, string param2)
        {

            string credential = authen.Authentication_Log(param1, param2);

            
            var listaURL = JsonConvert.DeserializeObject<datavalid.Receiver>(credential);
            string Token = listaURL.Personal_Token;
            var Username = listaURL.Username;

            if (credential != "")
            {
                Session["username"] = Username;
                Session["token"] = Token;

                string Rs = "Success";
                return Json(Rs);
            }
            else
            {
                string error = "Invalid Username and Password";
                return Json(error);
            }


        }


        /// <summary>
        /// to be able to do transaction
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="sname"></param>
        /// <param name="address"></param>
        /// <param name="email"></param>
        /// <param name="amount"></param>
        /// <param name="cardn"></param>
        /// <param name="carde"></param>
        /// <param name="cardtype"></param>
        /// <param name="cardcvv"></param>
        /// <param name="token"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult PaymentProcess(string fname, string sname, string address, string email, string amount, string cardn, string carde, string cardtype, string cardcvv, string token)
        {

            fname = datavalid.UppercaseFirst(fname);
            sname = datavalid.UppercaseFirst(sname);

            if (authen.PaymentProcess(fname, sname, address, email, amount, cardn, carde, cardtype, cardcvv, token))
            {

                string Rs = "Successful";//JsonConvert.SerializeObject(credential);
                return Json(Rs);
            }
            else
            {
                string error = "Unsucessful";
                return Json(error);
            }


        }


        /// <summary>
        /// to be able to get previous transaction
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        [HttpPost]
        public JsonResult GetPaymentDetails(string param)
        {

            string C1 = authen.GetPaymentDetails(param);

            if (valid.checkparamstring(param))
            {
                //string Rs = JsonConvert.SerializeObject(C1);
                return Json(C1);
            }
            else
            {
                return Json("No record is found");
            }


        }

    }
}