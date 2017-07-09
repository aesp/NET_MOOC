using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Repository.Database
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShoppingCartContext _context;

		public ProductRepository(ShoppingCartContext context)
		{
			_context = context;
		}

        public async Task<bool> Delete(long id)
        {
			try
			{
                var product = await Get(id);
                _context.Products.Remove(product);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
        }

        public async Task<Product> Get(long id)
        {
            return await _context.Products.SingleOrDefaultAsync(m => m.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return _context.Products;
        }

        public async Task<bool> Save(Product entity)
        {
            try{
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }catch(Exception){
                return false;
            }

        }

        public Task<IEnumerable<Product>> Search(string query)
        {
            var mQuery = query.ToLower();
            var results = _context.Products.Where(p => p.Name.ToLower().Contains(mQuery));
            return Task.FromResult<IEnumerable<Product>>(results);
        }

        public async Task<bool> Update(Product entity)
        {
			try
			{
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
        }
    }
}
