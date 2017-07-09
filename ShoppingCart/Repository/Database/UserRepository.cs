using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Jwt;
using ShoppingCart.Models;

namespace ShoppingCart.Repository.Database
{

    public class UserRepository : IUserRepository
    {
        private readonly ShoppingCartContext _context;

        public UserRepository(ShoppingCartContext context)
		{
			_context = context;
		}

        public async Task<User> Create(User user)
        {
            var tUser = await Login(user.Email, user.Password);
            if(tUser != null)
            {
                throw new Exception("User already exists");
            }

            try{
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }catch(Exception){
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return _context.Users;
        }

        public Task<User> GetProfile(string jwtToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(
                u => u.Email == email && u.Password == password
            );

            return user;
        }



        public Task<User> SocialAuth(string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
