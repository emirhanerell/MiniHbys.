using MiniHbys.Entity;

namespace MiniHbys.Business.Abstraction;

public interface IUserService
{
    void CreateUser(User user);
    User Login(string email,string password);
    bool IsUserExist(string email);
    void UpdateUser(User user);
    List<User> GetAllUsers();
    User GetUserById(int userId);
    void DeleteUser(int userId);
}