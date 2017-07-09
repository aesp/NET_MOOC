using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public partial class Payment
    {
        public Payment()
        {
        }

        [Key]
        public int IdPayment { get; set; }
        public int Amount { get; set; }
        public string Capture { get; set; } 
        public string Currency_code { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Installments { get; set; }
    }
}

