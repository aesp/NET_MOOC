using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers.Models
{


    public class ShoppingCartResponse
    {
        public class CartData{
            public string Id { get; set; }
            public IEnumerable<Product> Products { get; set; }
        }

        [JsonProperty(PropertyName = "data")]
        public CartData DataResponse { get; set; }
        public string Error { get; set; }
    }
}
