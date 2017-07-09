using System;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.Repository;
using ShoppingCart.Repository.Database;
using Xunit;

namespace ShoppingCart.Tests
{

    public class ProductTests
    {

        private readonly ShoppingCartContext _context;
        IProductRepository repository;

        public ProductTests()
        {
            var option = new DbContextOptionsBuilder<ShoppingCartContext>().UseSqlite(("Data Source=shoppingcart_core.db")).Options;

            _context = new ShoppingCartContext(option);
            repository = new ProductRepository(_context);
        }


        [Fact]
        public async void TestInsertProduct()
		{
            

            var product = new Product()
            {
                Name = "Test",
                Price = 30,
                ImageUrl = "http://test.com",
                Description = "desc"
            };

            await repository.Save(product);

            var newProduct = await _context.Products.SingleOrDefaultAsync(m => m.ProductId == product.ProductId);

            Assert.Equal(product.ProductId,newProduct.ProductId);
            Assert.Equal(product.Name, newProduct.Name);
            Assert.Equal(product.ImageUrl, newProduct.ImageUrl);
            Assert.Equal(product.Description, newProduct.Description);
            Assert.Equal(product.Price, newProduct.Price);

		}


		int Add(int x, int y)
		{
			return x + y;
		}
    }
}
