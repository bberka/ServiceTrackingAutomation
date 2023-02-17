using EasMe.EntityFrameworkCore.V2;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure.DAL;

public class ServiceRepository : GenericRepository<Service,BusinessDbContext>
{
    public ServiceRepository(BusinessDbContext context) : base(context)
    {
    }
}