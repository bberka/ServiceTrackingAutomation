using EasMe.Models;
using ServiceTrackingAutomation.Domain.DTOs;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Domain.Abstract;

public interface IServiceManager
{
    ResultData<Service> GetService(int id);
    List<Service> GetServices();
    Result UpdateService(Service service);
    Result AddService(Service service);
    Result DisableService(int id);
    Result EnableService(int id);

}