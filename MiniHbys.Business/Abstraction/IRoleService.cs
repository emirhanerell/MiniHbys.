using MiniHbys.Entity;

namespace MiniHbys.Business.Abstraction;

public interface IRoleService
{
    List<Role> GetAllRoles();
    void CreateRole(Role role);
    void UpdateRole(Role role);
    void DeleteRole(int roleId);
    Role GetRoleById(int roleId);
}