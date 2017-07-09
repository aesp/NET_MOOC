using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.Repository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> Search(string query);
    }
}
