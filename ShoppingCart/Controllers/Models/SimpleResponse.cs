using System;
namespace ShoppingCart.Controllers.Models
{
    public class SimpleResponse
    {
        public SimpleResponse()
        {
        }

        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
