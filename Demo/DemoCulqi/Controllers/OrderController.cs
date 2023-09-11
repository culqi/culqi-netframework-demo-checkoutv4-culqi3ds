using culqi.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Web.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
   //[Route("api/[controller]")]
    public class OrderController : ApiController
    {
        Security security = null;
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public ResponseCulqi Post([FromBody] dynamic json)
        {
            security = new Security();
            security.public_key = "pk_test_e94078b9b248675d";
            security.secret_key = "sk_test_c2267b5b262745f0";
            security.rsa_id = "de35e120-e297-4b96-97ef-10a43423ddec";
            security.rsa_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDswQycch0x/7GZ0oFojkWCYv+gr5CyfBKXc3Izq+btIEMCrkDrIsz4Lnl5E3FSD7/htFn1oE84SaDKl5DgbNoev3pMC7MDDgdCFrHODOp7aXwjG8NaiCbiymyBglXyEN28hLvgHpvZmAn6KFo0lMGuKnz8HiuTfpBl6HpD6+02SQIDAQAB";



            Int32 amount = json.amount;
            string currency_code = json.currency_code;
            string description = json.description;
            string order_number = json.order_number;


            DateTimeOffset fechaActual = DateTimeOffset.Now;   
            DateTimeOffset fechaMasUnDia = fechaActual.AddDays(1);           
            long epoch = fechaMasUnDia.ToUnixTimeSeconds();

            Dictionary<string, object> client_details = new Dictionary<string, object>
                {
                    {"first_name","Jordan"},
                    {"last_name", "Diaz Diaz"},                    
                    {"email", "jordandiaz2016@gmail.com"},
                    {"phone_number", "921484773"}
                }; 
          
            Dictionary<string, object> map = new Dictionary<string, object>
                {
                    {"amount",amount},
                    {"currency_code", currency_code},                    
                    {"description", description},
                    {"order_number", order_number},
                    {"client_details", client_details},
                    {"expiration_date", epoch}
                };

              ResponseCulqi json_object = new Order(security).Create(map);
              return json_object;
        }
      

    }
}

