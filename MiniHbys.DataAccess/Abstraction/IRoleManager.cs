using MiniHbys.Entity;

namespace MiniHbys.DataAccess.Abstraction;

public interface IRoleManager
{
    void CreateRole(Role role);
    void UpdateRole(Role role);
    Role GetRoleById(int roleId);
    List<Role> GetAllRoles();
    void DeleteRole(int roleId);
}