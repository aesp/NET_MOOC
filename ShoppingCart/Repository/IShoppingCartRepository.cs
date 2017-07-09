using System;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.Repository
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart.Models.ShoppingCart> GetCurrentCartForUser(string deviceId, string userId);
        Task<bool> AddToCart(string productId, string deviceId, string userId);
        Task<bool> RemoveFromCart(string productId, string deviceId, string userId);

    }
}
