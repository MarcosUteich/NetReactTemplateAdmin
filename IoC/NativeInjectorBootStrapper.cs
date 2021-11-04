using Business.Customers.Interfaces;
using Business.Customers.Services;
using Business.Dashboard.Interfaces;
using Business.Dashboard.Services;
using Business.Sales.Interfaces;
using Business.Sales.Services;
using Data.Repository;
using Domain.Data.Context;
using Domain.Data.ContextRepository;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //Repository
            services.AddSingleton<IMongoClient>(c =>
            {
                return new MongoClient(configuration["Database:ConnectionString"]);
            });

            services.AddScoped(c =>
                c.GetService<IMongoClient>().StartSession());

            services.AddScoped<IDatabaseContext, DataBaseContext>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IInstallationService, InstallationService>();

            //Business Customer
            services.AddScoped<ICustomerService, CustomerService>();

            //Business.Sales
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            //Business.Dashboard
            services.AddScoped<IOrderDashboardService, OrderDashboardService>();
            



        }
    }
}
