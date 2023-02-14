using culqi.net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;


namespace Demo4.Controllers
{
    public class JsonDemoController : Controller
    {


        /// <summary>  
        /// Welcome Note Message  
        /// </summary>  
        /// <returns>In a Json Format</returns>  
        public JsonResult WelcomeNote()
        {
            bool isAdmin = false;
            //TODO: Check the user if it is admin or normal user, (true-Admin, false- Normal user)  
            string output = isAdmin ? "Welcome to the Admin User" : "Welcome to the User";

            return Json(output, JsonRequestBehavior.AllowGet);
        }
        public String Card()
        {
            Security security = null;
            var rawRequestBody = new StreamReader(Request.InputStream).ReadToEnd();

            string[] valores = rawRequestBody.Split('&');
            string[] customer_id_arr = Convert.ToString(valores[4]).Split('=');
            string customer_id = customer_id_arr[1];
            string[] token_id_arr = Convert.ToString(valores[3]).Split('=');
            string token_id = token_id_arr[1];
            security = new Security();
            security.public_key = "pk_test_90667d0a57d45c48";
            security.secret_key = "sk_test_1573b0e8079863ff";

            

            if (valores.Length == 6)
            {
                Dictionary<string, object> map = new Dictionary<string, object>
                {
                     {"customer_id", customer_id},
                     {"token_id", token_id}
                };

                var json_object = JObject.Parse(new Card(security).Create(map));
                return json_object.ToString();
            }
            else
            {
                string[] eci_arr = Convert.ToString(valores[6]).Split('=');
                string eci = eci_arr[1];
                string[] xid_arr = Convert.ToString(valores[7]).Split('=');
                string xid = Convert.ToString(xid_arr[1]).Replace("%3D", "=");
                string[] cavv_arr = Convert.ToString(valores[8]).Split('=');
                string cavv = Convert.ToString(cavv_arr[1]).Replace("%3D", "=");
                string[] protocolVersion_arr = Convert.ToString(valores[9]).Split('=');
                string protocolVersion = protocolVersion_arr[1];
                string[] directoryServerTransactionId_arr = Convert.ToString(valores[10]).Split('=');
                string directoryServerTransactionId = directoryServerTransactionId_arr[1];
                Dictionary<string, object> authentication_3DS = new Dictionary<string, object>
                {
                    {"eci", eci},
                    {"xid", xid},
                    {"cavv", cavv},
                    {"protocolVersion", protocolVersion},
                    {"directoryServerTransactionId", directoryServerTransactionId}
                };
                Dictionary<string, object> map = new Dictionary<string, object>
                {
                    {"customer_id", customer_id},
                    {"token_id", token_id},
                    {"authentication_3DS", authentication_3DS},
                };
                var json_object = JObject.Parse(new Card(security).Create(map));
                return json_object.ToString();
            }
        }
        public String Customer()
        {

            string output = "Welcome to the Admin User";
            string jsonPostData;
            using (var stream = Request.InputStream)
            {
                stream.Position = 0;
                using (var reader = new System.IO.StreamReader(stream))
                {
                    jsonPostData = reader.ReadToEnd();
                }
            }

            JObject json = JObject.Parse(jsonPostData);
            Security security = null;
            string address = (string)json["address"]; 
            string address_city = (string)json["address_city"]; 
            string country_code = (string)json["country_code"];
            string email = (string)json["email"];
            string first_name = (string)json["first_name"];  
            string last_name = (string)json["last_name"]; 
            string phone_number = (string)json["phone_number"];
            security = new Security();
            security.public_key = "pk_test_90667d0a57d45c48";
            security.secret_key = "sk_test_1573b0e8079863ff";
            Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"address", address},
                {"address_city", address_city},
                {"country_code", country_code},
                {"email", email},
                {"first_name", first_name},
                {"last_name", last_name},
                {"phone_number", Convert.ToInt32(phone_number)}
            };

            var json_object = JObject.Parse(new Customer(security).Create(map));

            return Convert.ToString(json_object);
        }

        public String Charge()
        {
            var rawRequestBody = new StreamReader(Request.InputStream).ReadToEnd();

            string[] valores = rawRequestBody.Split('&');


            Security security = null;
            security = new Security();
            security.public_key = "pk_test_90667d0a57d45c48";
            security.secret_key = "sk_test_1573b0e8079863ff";

            string[] amount_arr = Convert.ToString(valores[0]).Split('=');
            string amount = amount_arr[1];
            string[] currency_code_arr = Convert.ToString(valores[1]).Split('=');
            string currency_code = currency_code_arr[1];          
            string[] email_arr = Convert.ToString(valores[2]).Split('=');
            string email = Convert.ToString(email_arr[1]).Replace("%40","@");
            string[] source_id_arr = Convert.ToString(valores[3]).Split('=');
            string source_id = source_id_arr[1];
          

            if (valores.Length == 6)
            {
                Dictionary<string, object> map = new Dictionary<string, object>
                {
                    {"amount", Convert.ToInt32(amount)},
                    {"currency_code", currency_code},                   
                    {"email", email},
                    {"source_id", source_id}
                };

                var json_object = JObject.Parse(new Charge(security).Create(map));
                return Convert.ToString(json_object);
            }
            else
            {
                string[] eci_arr = Convert.ToString(valores[6]).Split('=');
                string eci = eci_arr[1];
                string[] xid_arr = Convert.ToString(valores[7]).Split('=');
                string xid = Convert.ToString(xid_arr[1]).Replace("%3D", "=");
                string[] cavv_arr = Convert.ToString(valores[8]).Split('=');
                string cavv = Convert.ToString(cavv_arr[1]).Replace("%3D", "="); 
                string[] protocolVersion_arr = Convert.ToString(valores[9]).Split('=');
                string protocolVersion = protocolVersion_arr[1];
                string[] directoryServerTransactionId_arr = Convert.ToString(valores[10]).Split('=');
                string directoryServerTransactionId = directoryServerTransactionId_arr[1];
                Dictionary<string, object> authentication_3DS = new Dictionary<string, object>
                {
                    {"eci", eci},
                    {"xid", xid},
                    {"cavv", cavv},
                    {"protocolVersion", protocolVersion},
                    {"directoryServerTransactionId", directoryServerTransactionId}
                };
                Dictionary<string, object> map = new Dictionary<string, object>
                {
                    {"amount", Convert.ToInt32(amount)},
                    {"currency_code", currency_code},                   
                    {"email", email},
                    {"source_id", source_id},
                    {"authentication_3DS", authentication_3DS},
                };
                var json_object = JObject.Parse(new Charge(security).Create(map));

                return Convert.ToString(json_object);


            }
        }
    }
}