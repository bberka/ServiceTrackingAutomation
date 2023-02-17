using EasMe.EntityFrameworkCore.V2;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class CustomerContactRepository : GenericRepository<CustomerContact,BusinessDbContext>
{
    public CustomerContactRepository(BusinessDbContext context) : base(context)
    {
    }
}