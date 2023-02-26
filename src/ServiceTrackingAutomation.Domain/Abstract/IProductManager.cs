namespace ServiceTrackingAutomation.Domain.Abstract;

public interface IProductManager
{
    List<Product> GetProducts();
    List<Product> GetInvalidProducts();
    Result AddProduct(Product product);
    Result DisableProduct(int id);
    Result EnableProduct(int id);
    ResultData<Product> GetProduct(int id);
    Result UpdateProduct(Product product);

}