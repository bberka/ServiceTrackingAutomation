using EasMe.EntityFrameworkCore.V2;
using Microsoft.Extensions.DependencyInjection;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Infrastructure.DAL;

namespace ServiceTrackingAutomation.Infrastructure;

public static class DataAccessDependencyProvider
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddScoped<IGenericRepository<ChangeLog>, ChangeLogRepository>();
        services.AddScoped<IGenericRepository<Complaint>, ComplaintRepository>();
        services.AddScoped<IGenericRepository<CustomerContact>, CustomerContactRepository>();
        services.AddScoped<IGenericRepository<Customer>, CustomerRepository>();
        services.AddScoped<IGenericRepository<Product>, ProductRepository>();
        services.AddScoped<IGenericRepository<ProductType>, ProductTypeRepository>();
        services.AddScoped<IGenericRepository<ServiceAction>, ServiceActionRepository>();
        services.AddScoped<IGenericRepository<Service>, ServiceRepository>();
        services.AddScoped<IGenericRepository<User>, UserRepository>();
        services.AddDbContext<BusinessDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}