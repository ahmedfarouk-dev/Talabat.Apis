using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Errors;
using Talabat.Core.Interfaces;
using Talabat.Repositories;

namespace Talabat.Apis.Extension
{
    public static class ApplicationServicesExtension
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IRepositories<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IBasketRepositorycs), typeof(BasketRepository));


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
    }
}
