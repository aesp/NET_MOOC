using System;
using System.Threading.Tasks;
using System.Linq;
using ShoppingCart.Models;

namespace ShoppingCart.Repository.Database
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShoppingCartContext _context;
        public ShoppingCartRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        public async Task<Models.ShoppingCart> GetCurrentCartForUser(string deviceId, string userId)
		{
            try
			{
				if (userId != null)
				{
                    var cart = _context.ShoppingCarts.SingleOrDefault(
						c => c.User != null && c.User.Id == Int32.Parse(userId)
					);
                    /*
                    if(cart != null){
                        cart.ProductShoppingCarts = _context.ProductShoppingCarts
                            .Where(pc => pc.ShoppingCartId == cart.ShoppingCartId)
                            .ToList();
                    }*/

                    return cart;
				}
				else if (deviceId != null)
				{
					var cart = _context.ShoppingCarts.SingleOrDefault(
						c => c.ClientGuid == deviceId
					);

                    if(cart != null){
                        cart.Products = _context.ProductShoppingCarts
                            .Where(pc => pc.ShoppingCartId == cart.ShoppingCartId)
                            .Select(x => x.Product)
                            .ToList();
                    }

                    return cart;
				}
                return null;
            }
            catch(Exception e)
            {
                return null;
            }

        }

        public async Task<bool> AddToCart(string productId, string deviceId, string userId)
        {
			var cart = await GetCurrentCartForUser(deviceId, userId);
			if (cart == null)
			{
				cart = new Models.ShoppingCart();
                cart.ClientGuid = deviceId;

                if(userId != null)
                {
					try
					{
						cart.User = _context.Users.SingleOrDefault(u => u.Id == Int32.Parse(userId));
					}
					catch (Exception e)
					{
                        return false;
					}
                }

                _context.ShoppingCarts.Add(cart);
			}

            var item = _context.ProductShoppingCarts.SingleOrDefault(
                pc => pc.ShoppingCartId == cart.ShoppingCartId && pc.ProductId == Int32.Parse(productId)
            );

            if(item == null)
			{
				item = new ProductShoppingCart();
				item.ProductId = Int32.Parse(productId);
				item.ShoppingCartId = cart.ShoppingCartId;
				cart.ProductShoppingCarts.Add(item);

				await _context.SaveChangesAsync();
			}

            return true;
		}

        public async Task<bool> RemoveFromCart(string productId, string deviceId, string userId)
        {
            try
			{
				var cart = await GetCurrentCartForUser(deviceId, userId);
				if (cart != null)
				{
                    var item = _context.ProductShoppingCarts.SingleOrDefault(
                        i => i.ShoppingCartId == cart.ShoppingCartId && i.ProductId == Int32.Parse(productId)
                    );
                    _context.ProductShoppingCarts.Remove(item);
                    await _context.SaveChangesAsync();
                    return true;
                }else{
                    return false;
                }

            }catch(Exception){
                return false;
            }
			
        }
    }
}
