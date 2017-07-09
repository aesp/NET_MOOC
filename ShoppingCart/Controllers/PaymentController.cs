using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using culqi.net;
using Newtonsoft.Json.Linq;
using ShoppingCart.Models;


namespace ShoppingCart.Controllers
{
    [Produces("application/json")]
    [Route("api/Payment")]
    public class PaymentController : Controller
    {
        Security security = null;

        // PUT
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string token, [FromBody]Payment payment)
        {
            Security security = new Security();
            security.public_key = "pk_test_zfPcLV6oAKubZz0s";
            security.secret_key = "sk_test_HbgciH0NwUJi31L8";

            var json_object = JObject.Parse(token);
            Dictionary<string, object> metadata = new Dictionary<string, object>
            {
                {"order_id", "777"}
            };

            Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"amount", payment.Amount },
                {"capture", payment.Capture },
                {"currency_code", payment.Currency_code},
                {"description", payment.Description },
                {"email", payment.Email},
                {"installments", payment.Installments},
                {"metadata", metadata},
                {"source_id", (string)json_object["id"]}
            };
            var charge = new Charge(security).Create(map);
        }
        
    }
}
