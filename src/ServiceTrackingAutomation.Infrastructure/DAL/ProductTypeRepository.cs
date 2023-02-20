
namespace ServiceTrackingAutomation.Infrastructure.DAL;

internal class ProductTypeRepository : GenericRepository<ProductType,BusinessDbContext>
{
    public ProductTypeRepository(BusinessDbContext context) : base(context)
    {
    }
}