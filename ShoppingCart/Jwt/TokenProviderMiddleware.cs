﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShoppingCart.Models;
using ShoppingCart.Repository;

namespace ShoppingCart.Jwt
{

	public class TokenProviderMiddleware
	{

        private readonly RequestDelegate _next;
        private ITokenProvider _tokenProvider;
        private IUserRepository _repository;

        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options,
            ITokenProvider tokenProvider, 
            IUserRepository repository)
        {
            _next = next;
            _tokenProvider = tokenProvider;
            _repository = repository;
        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_tokenProvider.GetPath(), StringComparison.Ordinal))
            {
                return _next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST"))
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            return GenerateToken(context);
        }


        public async Task GenerateToken(HttpContext context)
		{
            TokenResponse response = null;
            try{

				var email = context.Request.Form["email"];
				var password = context.Request.Form["password"];

                var user = _repository.Login(email, password);
                response = await _tokenProvider.GetToken(user.Id.ToString());

			}catch(Exception){
                response = await _tokenProvider.GetToken();
            }
            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
		}



	}
}
