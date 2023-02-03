using eCommerce.BusinessLogic;
using eCommerce.BusinessLogic.ProductServices;
using eCommerce.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace eCommerce.Code.ExtentionMethods
{
    public static class ServiceCollectionExtentionMethods
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<UserAccountService>();
            services.AddScoped<ProductCategoryService>();
            services.AddScoped<ProductDetailsService>();
            services.AddScoped<ProductService>();
            services.AddScoped<AdminService>();
            services.AddScoped<PropertyService>();
            services.AddScoped<CartService>();
            services.AddScoped<UserService>();
            services.AddScoped<DeliveryLocationService>();
            services.AddScoped<ProductCommentService>();
            services.AddScoped<UserInvoiceService>();
            services.AddScoped<PasswordManagerService>();
            services.AddScoped<ManufacturerService>();
            services.AddScoped<PaymentService>();

            return services;
        }

        public static IServiceCollection AddCurrentUser(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor.HttpContext;

                return new CurrentUserDto
                {
                    IsAuthenticated = httpContext.User.Identity.IsAuthenticated,
                    Id = httpContext.User.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value,
                    Email = httpContext.User.Claims?.FirstOrDefault(c => c.Type == "Email")?.Value,
                    FirstName = httpContext.User.Claims?.FirstOrDefault(c => c.Type == "FirstName")?.Value,
                    LastName = httpContext.User.Claims?.FirstOrDefault(c => c.Type == "LastName")?.Value,
                    IsAdmin = httpContext.User.IsInRole("Admin")
                };
            });

            return services;
        }
    }
}
