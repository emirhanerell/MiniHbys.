using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class RolePermissionManager : IRolePermissionManager
{
    public void CreateRolePermission(RolePermissions rolePermissions)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"INSERT INTO RolePermission (RoleID,PermissionID) VALUES (@RoleID,@PermissionID)";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@RoleID", rolePermissions.RoleID);
                command.Parameters.AddWithValue("@PermissionID", rolePermissions.PermissionID);
                command.ExecuteNonQuery();
            }
        }
    }

    public void RemoveAllPermissionsFromRole(int roleId)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"DELETE FROM RolePermissions WHERE RoleID = @RoleID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@RoleID", roleId);
                command.ExecuteNonQuery();
            }
        }
    }
}