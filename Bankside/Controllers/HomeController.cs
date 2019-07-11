using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bankside.Models;

namespace Bankside.Controllers
{
    public class HomeController : Controller
    {
        CardValidation chk = new CardValidation();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        /// <summary>
        /// Check the user credit balance
        /// </summary>
        /// <param name="cardn"></param>
        /// <param name="cardcvv"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CheckCredit(string cardn,string cardcvv)
        {

            int cardCVV = Convert.ToInt32(cardcvv);
            string ResponseBalance = chk.CheckBalance(cardCVV, cardn);

            if (ResponseBalance != null )
            {

               
                return Json(ResponseBalance);
            }
            else
            {
                string error = "Error";
                return Json(error);
            }


        }

    }
}
