
namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class ServiceRepository : GenericRepository<Service,BusinessDbContext>
{
    public ServiceRepository(BusinessDbContext context) : base(context)
    {
    }
}