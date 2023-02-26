namespace ServiceTrackingAutomation.Application.Manager;

public class ProductManager : IProductManager
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public List<Product> GetProducts()
    {
        return _unitOfWork.ProductRepository.Get(x => x.IsValid == true).ToList();
    }

    public List<Product> GetInvalidProducts()
    {
        return _unitOfWork.ProductRepository.Get(x => x.IsValid == false).ToList();
    }

    public Result AddProduct(Product product)
    {
        product.IsValid = true;
        _unitOfWork.ProductRepository.Insert(product);
        return _unitOfWork.SaveResult(1);
    }

    public Result DisableProduct(int id)
    {
        var productResult = GetProduct(id);
        if (productResult.IsFailure)
        {
            return productResult.ToResult(100);
        }

        var product = productResult.Data;
        product.IsValid = false;
        _unitOfWork.ProductRepository.Update(product);
        return _unitOfWork.SaveResult(2);
    }

    public Result EnableProduct(int id)
    {
        var productResult = GetProduct(id);
        if (productResult.IsFailure)
        {
            return productResult.ToResult(100);
        }
        var product = productResult.Data;
        product.IsValid = true;
        _unitOfWork.ProductRepository.Update(product);
        return _unitOfWork.SaveResult(2);
    }

    public ResultData<Product> GetProduct(int id)
    {
        var product = _unitOfWork.ProductRepository.GetFirstOrDefault(x => x.Id == id);
        if (product is null)
        {
            return Result.Warn(1, "Ürün bulunamadı");
        }

        if (!product.IsValid)
        {
            return Result.Warn(2,"Ürün geçersiz");
        }
        return product;
    }

    public Result UpdateProduct(Product product)
    {
        var productResult = GetProduct(product.Id);
        if (productResult.IsFailure)
        {
            return productResult.ToResult(100);
        }
        var productDb = productResult.Data;
        productDb.Name = product.Name;
        productDb.Description = product.Description;
        _unitOfWork.ProductRepository.Update(productDb);
        return _unitOfWork.SaveResult(3);
    }
}