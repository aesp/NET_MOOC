using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.Jwt;
using ShoppingCart.Models;

namespace ShoppingCart.Repository
{
    public interface IUserRepository
    {
        Task<User> Login(String email, String password);
        Task<User> Create(User user);
        Task<User> SocialAuth(String accessToken);

        Task<IEnumerable<User>> GetAll();

	}


	public class UserExistsException : Exception{}
}
