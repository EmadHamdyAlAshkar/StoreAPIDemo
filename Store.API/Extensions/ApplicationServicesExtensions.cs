using Microsoft.AspNetCore.Mvc;
using Store.Repository.Intefraces;
using Store.Repository.Repositories;
using Store.Sevrice.Services.ProductService.Dtos;
using Store.Sevrice.Services.ProductService;
using Store.Sevrice.HandleResponses;
using Store.Sevrice.Services.CashService;
using Store.Repository.BasketRepository;
using Store.Sevrice.Services.BasketService.Dtos;
using Store.Sevrice.Services.BasketService;
using Store.Sevrice.Services.TokenService;
using Store.Sevrice.Services.UserService;
using Store.Sevrice.Services.OrderService.Dtos;

namespace Store.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICashService, CashService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(BasketProfile));
            services.AddAutoMapper(typeof(OrderProfile));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                    .Where(model => model.Value.Errors.Count > 0)
                                    .SelectMany(model => model.Value.Errors)
                                    .Select(error => error.ErrorMessage)
                                    .ToList();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);

                };
            });

            return services;
        }
    }
}
