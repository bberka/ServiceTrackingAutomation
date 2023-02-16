using EasMe.Models;
using ServiceTrackingAutomation.Domain.DTOs;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Domain.Abstract;

public interface IServiceManager
{
    ResultData<ServiceDto> GetService(int id);
    List<ServiceDto> GetServices();
    Result UpdateService(ServiceDto service);
    Result AddService(ServiceDto service);
    Result DisableService(int id);
    Result DeleteService(int id);
    Result EnableService(int id);

}