using EasMe.Models;
using ServiceTrackingAutomation.Domain.Entities;
using ServiceTrackingAutomation.Domain.Models;

namespace ServiceTrackingAutomation.Domain.Abstract;

public interface IAuthManager
{
    ResultData<User> Authenticate(LoginModel model);
}