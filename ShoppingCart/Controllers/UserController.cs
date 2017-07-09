using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Controllers.Models;
using ShoppingCart.Jwt;
using ShoppingCart.Models;
using ShoppingCart.Repository;
using ShoppingCart.Repository.Database;

namespace ShoppingCart.Controllers
{

	[EnableCors("SiteCorsPolicy")]
	[Produces("application/json")]
	[Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserRepository repository;
        private readonly ITokenProvider tokenProvider;

        public UserController(IUserRepository repository, 
                              ITokenProvider tokenProvider)
        {
            this.repository = repository;
            this.tokenProvider = tokenProvider;
        }

		// GET: api/users/create
        [HttpGet("generateToken")]
        public async Task<TokenResponse> GenerateToken()
        {
            return await tokenProvider.GetToken();
        }


		// GET: api/users/create
		[HttpPost("create")]
        public async Task<TokenResponse> Create([FromBody] CreateRequest request)
        {
            try
            {
                var user = new User()
                {
                    Email = request.email,
                    Password = request.password,
                    FirstName = request.firstName,
                    LastName = request.lastName
                };

                var created = await repository.Create(user);
                return await tokenProvider.GetToken(created.Id.ToString());
            }
            catch (UserExistsException)
            {
                Response.StatusCode = 409;
                return new TokenResponse()
                {
                    Error = "User already exists"
                };
            }
            catch (Exception e)
            {
                return new TokenResponse()
                {
                    Error = e.Message
                };
            }
        }



        // POST: api/users/login
        [HttpPost("login")]
        public async Task<TokenResponse> Login([FromBody] LoginRequest request)
        {
            var user = await repository.Login(request.email,request.password);
            if (user != null)
            {
                return await tokenProvider.GetToken(user.Id.ToString());
            }
            else
            {
                Response.StatusCode = 422;
                return new TokenResponse()
                {
                    Error = "Invalid credentials"
                };
            }
        }

		// GET: api/users/social
		[HttpPost("social")]
        public async Task<TokenResponse> SocialAuth([FromBody] String accessToken)
        {
            Response.StatusCode = 404;
			return new TokenResponse()
			{
				Error = "NotImplementedException"
			};
        }

    }
}
