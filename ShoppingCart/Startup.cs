using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShoppingCart.Models;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Cors.Infrastructure;
using ShoppingCart.Repository;
using ShoppingCart.Repository.Database;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ShoppingCart.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ShoppingCart
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // ********************
            // Setup CORS
            // ********************
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.WithOrigins("https://localhost:4430");
            //corsBuilder.AllowAnyOrigin(); // For anyone access.
                                          //corsBuilder.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

			services.AddAuthorization(auth =>
			{
				auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
					.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
					.RequireAuthenticatedUser().Build());
			});

            // Add framework services.
            //services.AddDbContext<ShoppingCartContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));


            services.AddEntityFrameworkSqlite()
                    .AddDbContext<ShoppingCartContext>(option =>
                    {
                        option.UseSqlite((Configuration["Data:DefaultConnection:ConnectionString"]));
                    });


            services.AddMvc().AddJsonOptions(a => a.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddSingleton<IProductRepository, ProductRepository>();
			services.AddSingleton<ITokenProvider>(new JwtTokenProvider(GetTokenOptions()));
			services.AddSingleton<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSwaggerGen(c =>{c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

			var options = GetTokenOptions();
			var tokenValidationParameters = new TokenValidationParameters
			{
				// The signing key must match!
				ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecretKey(),

                ValidateIssuer = true,
                ValidIssuer = options.Issuer,

                ValidateAudience = false

				// Validate the token expiry
				//ValidateLifetime = true
			};


			app.UseJwtBearerAuthentication(new JwtBearerOptions
			{
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				TokenValidationParameters = tokenValidationParameters
			});



			/*app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AutomaticAuthenticate = true,
				AutomaticChallenge = true,
				AuthenticationScheme = "Cookie",
				CookieName = "access_token",
				TicketDataFormat = new CustomJwtDataFormat(
					SecurityAlgorithms.HmacSha256,
					tokenValidationParameters)
			});*/

            app.UseCors("SiteCorsPolicy");

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(GetTokenOptions()));

            app.UseMvc();
            app.UseMvcWithDefaultRoute();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }

        private SymmetricSecurityKey GetSecretKey()
        {
			var secretKey = "mysupersecret_secretkey!123";
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }

        public TokenProviderOptions GetTokenOptions()
        {
            return new TokenProviderOptions
            {
                SigningCredentials = new SigningCredentials(GetSecretKey(), SecurityAlgorithms.HmacSha256),
                Expiration = TimeSpan.FromHours(24),
                Issuer = "ShoppingCart"
            };
        }
    }
}
