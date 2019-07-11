using Newtonsoft.Json;
using System;
using System.Net;

namespace ClientSide.Models
{
    public class Autho
    {

        /// <summary>
        /// Point of contact to login and retrieved your Personal Token
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public string Authentication_Log(string param1, string param2)
        {
            try
            {



                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:44379/api/ApiUserRegister?username=" + param1 + "&password=" + param2);
                request.Method = "GET";
                request.KeepAlive = true;
                request.ContentType = "application/json";
                // request.Headers.Add("Content-Type", "application/json");
                //request.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string myResponse = "";
                System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream());
                myResponse = sr.ReadToEnd();

                return myResponse;

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Point of contact to post your transaction
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
        public bool PaymentProcess(string fname, string sname, string address, string email, string amount, string cardn, string carde, string cardtype, string cardcvv, string token)
        {
            try
            {
                using (WebClient client = new WebClient())
                {

                    var data = new
                    {
                        Firstname = fname,
                        Surname = sname,
                        Address = address,
                        Email = email,
                        CardType = cardtype,
                        Currency = "USD",
                        Personal_Token = token,
                        Card_Number = cardn,
                        Amount = amount,
                        Card_Expiration = carde,
                        Card_cvv = cardcvv

                    };


                    var dataString = JsonConvert.SerializeObject(data);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var ApiResponse = client.UploadString(new Uri("https://localhost:44379/api/ApiPayments"), "POST", dataString);
                    var SerializeValue = JsonConvert.DeserializeObject(ApiResponse);
                    if (SerializeValue.ToString() == "Successful")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            catch (Exception er)
            {
                return false;
            }


        }

        /// <summary>
        /// Point of contact to able to register
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public bool ApiRegistration(string param1, string param2)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    var data = new
                    {
                        Username = param1,
                        Password = param2
                    };

                    var dataString = JsonConvert.SerializeObject(data);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                    var RegisterResponse = client.UploadString(new Uri("https://localhost:44379/api/ApiUserRegister"), "POST", dataString);
                    var SerializeValue = JsonConvert.DeserializeObject(RegisterResponse);

                    if (SerializeValue.ToString() == "Registered")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception er)
            {
                return false;
            }
        }



        /// <summary>
        /// Point of Contact where to retrieved previous transaction
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetPaymentDetails(string param)
        {
            try
            {


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:44379/api/ApiPayments?token=" + param);
                request.Method = "GET";
                request.KeepAlive = true;
                request.ContentType = "application/json";
                // request.Headers.Add("Content-Type", "application/json");
                //request.ContentType = "application/x-www-form-urlencoded";


                string myResponse = "";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream());
                myResponse = sr.ReadToEnd();
                return myResponse;

            }
            catch (Exception er)
            {
                return null;
            }
        }
    }
}