using MiniHbys.Entity;

namespace MiniHbys.DataAccess.Abstraction;

public interface IPermissionManager
{
    List<Permission> GetAllPermissions();
    Permission GetPermissionById(int permissionId);
}