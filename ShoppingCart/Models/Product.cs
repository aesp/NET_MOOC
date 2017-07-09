using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ShoppingCart.Models
{
    public partial class Product
    {
        public Product()
        {
        }

        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductShoppingCart> ProductShoppingCarts { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Product))
                return false;

            var other = (Product)obj;
            return Name == other.Name && ProductId == other.ProductId
                                && Price == other.Price && ImageUrl == other.ImageUrl
                                && Description == other.Description;
        }


    }
}
