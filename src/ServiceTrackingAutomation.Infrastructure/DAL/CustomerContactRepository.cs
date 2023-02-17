using EasMe.EntityFrameworkCore.V2;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure.DAL;

public class CustomerContactRepository : GenericRepository<CustomerContact,BusinessDbContext>
{
    public CustomerContactRepository(BusinessDbContext context) : base(context)
    {
    }
}