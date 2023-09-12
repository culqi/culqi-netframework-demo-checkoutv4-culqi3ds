using culqi.net;
using Newtonsoft.Json.Linq;
using RestSharp;
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
   
   ///[Route("api/[controller]")]
    public class CustomerController : ApiController
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
        public RestResponse Post([FromBody] dynamic json)
        {
            security = new Security();
            security.public_key = "pk_test_e94078b9b248675d";
            security.secret_key = "sk_test_c2267b5b262745f0";
            security.rsa_id = "de35e120-e297-4b96-97ef-10a43423ddec";
            security.rsa_key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDswQycch0x/7GZ0oFojkWCYv+gr5CyfBKXc3Izq+btIEMCrkDrIsz4Lnl5E3FSD7/htFn1oE84SaDKl5DgbNoev3pMC7MDDgdCFrHODOp7aXwjG8NaiCbiymyBglXyEN28hLvgHpvZmAn6KFo0lMGuKnz8HiuTfpBl6HpD6+02SQIDAQAB";


            string address = json.address;
            string address_city = json.address_city;
            string country_code = json.country_code;
            string email = json.email;
            string first_name = json.first_name;
            string last_name = json.last_name;
            string phone_number = json.phone_number;

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

            RestResponse json_object = new Customer(security).Create(map);
            return json_object;
        }


      

    }
}

