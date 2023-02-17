using EasMe.EntityFrameworkCore.V2;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure.DAL;

public class CustomerRepository : GenericRepository<Customer,BusinessDbContext>
{
    public CustomerRepository(BusinessDbContext context) : base(context)
    {
    }
}