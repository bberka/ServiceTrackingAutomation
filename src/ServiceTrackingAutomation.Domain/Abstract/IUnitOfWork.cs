using System.Runtime.CompilerServices;
using EasMe.EntityFrameworkCore.V2;
using EasMe.Models;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Domain.Abstract;

public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<ChangeLog> ChangeLogRepository { get; }
    public IGenericRepository<Complaint> ComplaintRepository { get; }
    public IGenericRepository<ComplaintProduct> ComplaintProductRepository { get; }
    public IGenericRepository<Customer> CustomerRepository { get; }
    public IGenericRepository<Product> ProductRepository { get; }
    public IGenericRepository<Service> ServiceRepository { get; }
    public IGenericRepository<ServiceAction> ServiceActionRepository { get; }
    public IGenericRepository<User> UserRepository { get; }
    public bool IsDisposed { get; }
    int Save();
    bool SaveBool();
    Result SaveResult(ushort rv, [CallerMemberName] string operationName = "");
}