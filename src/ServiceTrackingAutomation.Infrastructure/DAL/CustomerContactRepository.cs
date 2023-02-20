
namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class CustomerContactRepository : GenericRepository<CustomerContact,BusinessDbContext>
{
    public CustomerContactRepository(BusinessDbContext context) : base(context)
    {
    }
}