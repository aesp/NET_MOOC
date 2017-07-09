using System;
namespace ShoppingCartWeb.Models.ProductViewModels
{
	public class ProductViewModel
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
    }
}
