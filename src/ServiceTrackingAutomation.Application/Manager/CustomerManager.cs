namespace ServiceTrackingAutomation.Application.Manager;

public class CustomerManager : ICustomerManager
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public List<Customer> GetCustomers()
    {
        return _unitOfWork.CustomerRepository.Get(x => x.IsValid == true).ToList();
    }

    public List<Customer> GetInvalidCustomers()
    {
        return _unitOfWork.CustomerRepository.Get(x => x.IsValid == false).ToList();
    }

    public Result AddCustomer(Customer customer)
    {
        customer.IsValid = true;
        _unitOfWork.CustomerRepository.Insert(customer);
        return _unitOfWork.SaveResult(1);

    }

    public Result DisableCustomer(int id)
    {
        var customerResult = GetCustomer(id);
        if (customerResult.IsFailure)
        {
            return customerResult.ToResult(100);
        }

        var customer = customerResult.Data;
        customer.IsValid = false;
        _unitOfWork.CustomerRepository.Update(customer);
        return _unitOfWork.SaveResult(2);
    }

    public Result EnableCustomer(int id)
    {
        var customerResult = GetCustomer(id);
        if (customerResult.IsFailure)
        {
            return customerResult.ToResult(100);
        }

        var customer = customerResult.Data;
        customer.IsValid = true;
        _unitOfWork.CustomerRepository.Update(customer);
        return _unitOfWork.SaveResult(2);
    }

    public ResultData<Customer> GetCustomer(int id)
    {
        var customer = _unitOfWork.CustomerRepository.Get(x => x.Id == id).FirstOrDefault();
        if (customer == null)
        {
            return Result.Warn(1, "Müşteri bulunamadı");
        }
        if (!customer.IsValid)
        {
            return Result.Warn(2, "Müşteri geçersiz");
        }
        return customer;
    }

    public Result DeleteCustomer(int id)
    {
        var customerResult = GetCustomer(id);
        if (customerResult.IsFailure)
        {
            return customerResult.ToResult(100);
        }
        var customer = customerResult.Data;
        _unitOfWork.CustomerRepository.Delete(customer);
        return _unitOfWork.SaveResult(2);
    }

    public Result UpdateCustomer(Customer customer)
    {
        var customerResult = GetCustomer(customer.Id);
        if (customerResult.IsFailure)
        {
            return customerResult.ToResult(100);
        }
        var customerDb = customerResult.Data;
        customerDb.Name = customer.Name;
        customerDb.PhoneNumber = customer.PhoneNumber;
        customerDb.EmailAddress = customer.EmailAddress;
        customerDb.Address = customer.Address;
        _unitOfWork.CustomerRepository.Update(customerDb);
        return _unitOfWork.SaveResult(2);
    }
}