using MiniHbys.Business.Abstraction;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Business.Services;

public class RoleService : IRoleService
{
    private readonly IRoleManager _roleManager;
    public RoleService(IRoleManager roleManager)
    {
        _roleManager = roleManager;
    }
    
    public List<Role> GetAllRoles()
    {
        return _roleManager.GetAllRoles();
    }

    public void CreateRole(Role role)
    {
        _roleManager.CreateRole(role);
    }
    public void UpdateRole(Role role)
    {
        _roleManager.UpdateRole(role);
    }

    public void DeleteRole(int roleId)
    {
        var doctor = _roleManager.GetRoleById(roleId);

        if (doctor == null)
            throw new Exception("Couldnt find role with department id :"+roleId);
        
        _roleManager.DeleteRole(roleId);
    }
    public Role GetRoleById(int roleId)
    {
        return _roleManager.GetRoleById(roleId);
    }
}