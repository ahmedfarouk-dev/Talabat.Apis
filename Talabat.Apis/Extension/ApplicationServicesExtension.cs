using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Apis.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Core.Services;
using Talabat.Repositories;
using Talabat.Repositories.Identity;
using Talabat.Services;

namespace Talabat.Apis.Extension
{
    public static class ApplicationServicesExtension
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IRepositories<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(IAuthServices), typeof(AuthServices));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IOrderService), typeof(OrderServices));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(p => p.Value.Errors.Count() > 0)
                    .SelectMany(p => p.Value.Errors).Select(p => p.ErrorMessage).ToList();
                    var response = new ApiValidationErrorHandling()
                    {

                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        public static IServiceCollection AddIdentutyAndJwtApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<UserApplication, IdentityRole>()

                .AddEntityFrameworkStores<StoreIdentity>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"], // Issuer
                        ValidAudience = configuration["JwtSettings:Audience"], // Audience
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                });

            return services;
        }

    }
}
