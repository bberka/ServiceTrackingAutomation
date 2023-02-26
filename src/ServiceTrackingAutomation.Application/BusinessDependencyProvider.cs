using Microsoft.Extensions.DependencyInjection;
using ServiceTrackingAutomation.Application.Manager;
using ServiceTrackingAutomation.Domain.Abstract;

namespace ServiceTrackingAutomation.Application;

public static class BusinessDependencyProvider
{
    public static IServiceCollection AddBusinessDependency(this IServiceCollection services)
    {
        services.AddScoped<IAuthManager, AuthManager>();
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IProductManager, ProductManager>();
        services.AddScoped<ICustomerManager, CustomerManager>();
        services.AddScoped<IComplaintManager, ComplaintManager>();
        services.AddScoped<IServiceActionManager, ServiceActionManager>();
        return services;
    }
}