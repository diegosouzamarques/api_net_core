using ApiDotNet6.Application.Mappings;
using ApiDotNet6.Application.Services;
using ApiDotNet6.Application.Services.Interface;
using ApiDotNet6.Domain.Authentication;
using ApiDotNet6.Domain.Repositories;
using ApiDotNet6.Infra.Data.Authentication;
using ApiDotNet6.Infra.Data.Context;
using ApiDotNet6.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection addInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DatabaseDefault")));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonImageRepository, PersonImageRepository>();
           
            return services;
        }

        public static IServiceCollection addServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPersonImageService, PersonImageService>();

            return services;
        }
    }
}
