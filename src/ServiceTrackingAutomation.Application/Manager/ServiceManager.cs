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

        public ResultData<ServiceDto> GetService(int id)
        {
            var service = _unitOfWork.ServiceRepository.GetFirstOrDefault(x => x.IsValid == true,nameof(ServiceAction));
            
            if(service is null) return Result.Warn(1,"Servis bulunamadı");
            return new ServiceDto
            {
                Name = service.Name,
                Address = service.Address,
                Id = service.Id,
                PhoneNumber = service.PhoneNumber
            };
        }

        public List<ServiceDto> GetServices()
        {
            return _unitOfWork.ServiceRepository
                .Get(x => x.IsValid == true,null,x => x.ServiceAction)
                .Select(x => new ServiceDto()
                {
                    Address = x.Address,
                    Id = x.Id,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber
                })
                .ToList();
        }

        public Result UpdateService(ServiceDto serviceDto)
        {
            var existing = _unitOfWork.ServiceRepository.GetById(serviceDto.Id);
            if (existing is null)
            {
                return Result.Warn(1, "Servis bulunamadı");
            }

            if (!existing.IsValid)
            {
                return Result.Warn(2, "Servis geçersiz");
            }
            existing.Address = serviceDto.Address;
            existing.Name = serviceDto.Name;
            existing.PhoneNumber = serviceDto.PhoneNumber;
            _unitOfWork.ServiceRepository.Update(existing);
            return _unitOfWork.SaveResult(3,nameof(UpdateService));
        }

        public Result AddService(ServiceDto serviceDto)
        {
            var service = new Service()
            {
                Address = serviceDto.Address,
                IsValid = true,
                Name = serviceDto.Name,
                PhoneNumber = serviceDto.PhoneNumber,
            };
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
