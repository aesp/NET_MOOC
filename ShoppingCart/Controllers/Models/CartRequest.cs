using System;
namespace ShoppingCart.Controllers.Models
{
    public class CartRequest
    {
        public CartRequest()
        {
        }

        public string productId { get; set; }
        public string deviceId { get; set; }
        public string userId { get; set; }

    }
}
