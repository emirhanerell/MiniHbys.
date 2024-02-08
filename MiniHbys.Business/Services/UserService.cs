using MiniHbys.Business.Abstraction;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.DataAccess.Managers;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.Business.Services;

public class UserService : IUserService
{
    private readonly IUserManager _userManager;
    public UserService(IUserManager userManager)
    {
        _userManager = userManager;
    }

    public void CreateUser(User user)
    {
        var encryptedPassword = EncyptionHelper.Encrypt(user.Password, AppConstants.EncryptionKey);
        user.Password = encryptedPassword;
       _userManager.CreateUser(user);
    }

    public User Login(string email, string password)
    {
        var encryptedPassword = EncyptionHelper.Encrypt(password, AppConstants.EncryptionKey);
        var user = _userManager.Login(email, encryptedPassword);
        return user;
    }

    public bool IsUserExist(string email)
    {
        return _userManager.IsUserExist(email);
    }

    public void UpdateUser(User user)
    {
        var encryptedPassword = EncyptionHelper.Encrypt(user.Password, AppConstants.EncryptionKey);
        user.Password = encryptedPassword;
        _userManager.UpdateUser(user);
    }
    
    public List<User> GetAllUsers()
    {
        return _userManager.GetAllUsers();
    }
    
    public User GetUserById(int userId)
    {
        return _userManager.GetUserById(userId);
    }

    public void DeleteUser(int userId)
    {
        _userManager.DeleteUser(userId);
    }

}