using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models
{
    public class ShoppingCart
    {

        public ShoppingCart()
        {
            ProductShoppingCarts = new HashSet<ProductShoppingCart>();
        }

        [Key]
        public int ShoppingCartId { get; set; }
        public string ClientGuid { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ProductShoppingCart> ProductShoppingCarts { get; set; }

        [NotMapped]
        public ICollection<Product> Products { get; set; }
    }
}
