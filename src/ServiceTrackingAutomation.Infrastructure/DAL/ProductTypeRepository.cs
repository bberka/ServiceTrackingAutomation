using EasMe.EntityFrameworkCore.V2;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Infrastructure.DAL;

public class ProductTypeRepository : GenericRepository<ProductType,BusinessDbContext>
{
    public ProductTypeRepository(BusinessDbContext context) : base(context)
    {
    }
}