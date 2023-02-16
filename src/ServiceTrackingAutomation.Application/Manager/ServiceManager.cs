using EasMe.Extensions;
using EasMe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ServiceTrackingAutomation.Domain.Abstract;
using ServiceTrackingAutomation.Domain.DTOs;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Infrastructure;

namespace ServiceTrackingAutomation.Application.Manager
{
    public class ServiceManager : IServiceManager
    {
        private readonly BusinessDbContext _dbContext;

        public ServiceManager(BusinessDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultData<ServiceDto> GetService(int id)
        {
            var service = _dbContext.Services
                .Where(x => x.IsValid == true)
                .Include(x => x.ServiceAction)
                .Select(x => new ServiceDto()
                {
                    Name = x.Name,
                    Address = x.Address,
                    Id = x.Id,
                    PhoneNumber = x.PhoneNumber
                })
                .FirstOrDefault();
            if(service is null) return Result.Warn(1,"Servis bulunamadı");
            return service;
        }

        public List<ServiceDto> GetServices()
        {
            return _dbContext.Services
                .Where(x => x.IsValid == true)
                .Include(x => x.ServiceAction)
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
            var existing = _dbContext.Services.Find(serviceDto.Id);
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
            _dbContext.Services.Update(existing);
            return _dbContext.SaveChangesResult(3,nameof(UpdateService));
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
            _dbContext.Services.Add(service);
            return _dbContext.SaveChangesResult(1);
        }

        public Result DisableService(int id)
        {
            var service =  _dbContext.Services.Find(id);
            if (service is null)
            {
                return Result.Warn(1, "Servis bulunamadı");
            }

            if (!service.IsValid)
            {
                return Result.Warn(2, "Servis zaten geçersiz");
            }
            service.IsValid = false;
            _dbContext.Update(service);
            return _dbContext.SaveChangesResult(3);
        }

        public Result DeleteService(int id)
        {
            var service = _dbContext.Services.Find(id);
            if (service is null)
            {
                return Result.Warn(1, "Servis bulunamadı");
            }
            _dbContext.Remove(service);
            return _dbContext.SaveChangesResult(2);
        }

        public Result EnableService(int id)
        {
            var service = _dbContext.Services.Find(id);
            if (service is null)
            {
                return Result.Warn(1, "Servis bulunamadı");
            }

            if (service.IsValid)
            {
                return Result.Warn(2, "Servis zaten geçersiz");
            }
            service.IsValid = true;
            _dbContext.Update(service);
            return _dbContext.SaveChangesResult(3);
        }
    }


}
