using System;
namespace ShoppingCart.Controllers.Models
{
    public class CreateRequest
	{
		public string email { get; set; }
		public string password { get; set; }
        public string firstName { get; set; }
		public string lastName { get; set; }
    }
}
