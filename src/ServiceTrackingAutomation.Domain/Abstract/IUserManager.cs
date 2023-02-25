

using ServiceTrackingAutomation.Domain.Models;

namespace ServiceTrackingAutomation.Domain.Abstract;

public interface IUserManager
{
    ResultData<User> GetUser(int id);
    ResultData<User> GetUserByEmail(string email);
    Result UpdateUser(User user);
    Result DeleteUser(int id);
    Result DisableUser(int id);
    Result EnableUser(int id);
    List<User> GetUsers(int exceptUserId);
    List<User> GetInvalidUsers();
    Result ChangePassword(int userId, ChangePasswordModel model);
    Result CreateUser(User user);

}