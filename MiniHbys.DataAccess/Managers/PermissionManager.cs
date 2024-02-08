using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class PermissionManager : IPermissionManager
{
    public List<Permission> GetAllPermissions()
    {
        var permissions = new List<Permission>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM Permission";
            using (var command = new SqlCommand(commandText,connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var permission = new Permission();
                    permission.PermissionCode = reader["PermissionCode"] != DBNull.Value
                        ? reader["PermissionCode"]
                            .ToString()
                        : string.Empty;
                    permission.PermissionDescription = reader["PermissionDescription"] != DBNull.Value
                        ? reader["PermissionDescription"].ToString()
                        : string.Empty;
                    permission.PermissionID = reader["PermissionID"].ToInt32();
                    permissions.Add(permission);
                }
            }
        }

        return permissions;
    }

    public Permission GetPermissionById(int permissionId)
    {
        Permission permission = null;
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@PermissionID", permissionId);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    permission = new Permission();
                    permission.PermissionCode = reader["PermissionCode"] != DBNull.Value
                        ? reader["PermissionCode"]
                            .ToString()
                        : string.Empty;
                    permission.PermissionDescription = reader["PermissionDescription"] != DBNull.Value
                        ? reader["PermissionDescription"].ToString()
                        : string.Empty;
                    permission.PermissionID = reader["PermissionID"].ToInt32();
                }
            }
        }
        return permission;
    }

    public List<Permission> GetPermissionsByRoleId(int roleId)
    {
        var permissions = new List<Permission>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT p.*  FROM Permission p INNER JOIN RolePermissions rp ON rp.PermissionID =p
            .PermissionID  where rp.RoleID  = @RoleID ";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@RoleID", roleId);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var permission = new Permission();
                    permission.PermissionCode = reader["PermissionCode"] != DBNull.Value
                        ? reader["PermissionCode"]
                            .ToString()
                        : string.Empty;
                    permission.PermissionDescription = reader["PermissionDescription"] != DBNull.Value
                        ? reader["PermissionDescription"].ToString()
                        : string.Empty;
                    permission.PermissionID = reader["PermissionID"].ToInt32();
                    permissions.Add(permission);
                }
            }
        }
        return permissions;
    }
}