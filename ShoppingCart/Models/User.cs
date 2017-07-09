using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ShoppingCart.Models
{
    public class User
    {

        public User()
        {
            Carts = new HashSet<ShoppingCart>();
        }

        [Key]
        public int Id { get; set; }
		public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool EmailConfirmed { get; set; }

        public virtual ICollection<ShoppingCart> Carts { get; set; }
    }
}
