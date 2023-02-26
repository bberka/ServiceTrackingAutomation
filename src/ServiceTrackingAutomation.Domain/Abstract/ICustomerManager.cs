namespace ServiceTrackingAutomation.Domain.Abstract;

public interface ICustomerManager
{
    List<Customer> GetCustomers();
    List<Customer> GetInvalidCustomers();
    Result AddCustomer(Customer customer);
    Result DisableCustomer(int id);
    Result EnableCustomer(int id);
    ResultData<Customer> GetCustomer(int id);
    Result DeleteCustomer(int id);
    Result UpdateCustomer(Customer customer);

}