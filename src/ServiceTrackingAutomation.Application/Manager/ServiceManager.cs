using EasMe.Models;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.DTOs;
using ServiceTrackingAutomation.Domain.Entities;

namespace ServiceTrackingAutomation.Application.Manager
{
    public class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResultData<Service> GetService(int id)
        {
            var service = _unitOfWork.ServiceRepository.GetFirstOrDefault(x => x.IsValid == true,nameof(ServiceAction));
            
            if(service is null) return Result.Warn(1,"Servis bulunamadı");
            return service;
        }

        public List<Service> GetServices()
        {
            return _unitOfWork.ServiceRepository
                .Get(x => x.IsValid == true)
                .ToList();
        }

        public Result UpdateService(Service service)
        {
            var existing = _unitOfWork.ServiceRepository.GetById(service.Id);
            if (existing is null)
            {
                return Result.Warn(1, "Servis bulunamadı");
            }

            if (!existing.IsValid)
            {
                return Result.Warn(2, "Servis geçersiz");
            }
            existing.Address = service.Address;
            existing.Name = service.Name;
            existing.PhoneNumber = service.PhoneNumber;
            _unitOfWork.ServiceRepository.Update(existing);
            return _unitOfWork.SaveResult(3);
        }

        public Result AddService(Service service)
        {
            _unitOfWork.ServiceRepository.Insert(service);
            return _unitOfWork.SaveResult(1);
        }

        public Result DisableService(int id)
        {
            var service =  _unitOfWork.ServiceRepository.GetById(id);
            if (service is null)
            {
                return Result.Warn(1, "Servis bulunamadı");
            }

            if (!service.IsValid)
            {
                return Result.Warn(2, "Servis zaten geçersiz");
            }
            service.IsValid = false;
            _unitOfWork.ServiceRepository.Update(service);
            return _unitOfWork.SaveResult(3);
        }

        public Result DeleteService(int id)
        {
            var service = _unitOfWork.ServiceRepository.GetById(id);
            if (service is null)
            {
                return Result.Warn(1, "Servis bulunamadı");
            }
            _unitOfWork.ServiceRepository.Delete(service);
            return _unitOfWork.SaveResult(2);
        }

        public Result EnableService(int id)
        {
            var service = _unitOfWork.ServiceRepository.GetById(id);
            if (service is null)
            {
                return Result.Warn(1, "Servis bulunamadı");
            }

            if (service.IsValid)
            {
                return Result.Warn(2, "Servis zaten geçersiz");
            }
            service.IsValid = true;
            _unitOfWork.ServiceRepository.Update(service);
            return _unitOfWork.SaveResult(3);
        }
    }


}
