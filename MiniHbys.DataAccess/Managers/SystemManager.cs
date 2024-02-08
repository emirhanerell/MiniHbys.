using System.Data;
using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class SystemManager : ISystemManager    
{
    public void CreateBackup(string backupPath,string fileName)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = "sp_backup";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Path", backupPath);
                command.Parameters.AddWithValue("@FileName", fileName);
                command.ExecuteNonQuery();
            }
        }
    }

    public void RestoreBackup(string fullPath)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString.Replace("MiniHbys","master")))
        {
            connection.Open();
            var commandText = "sp_restore";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.CommandTimeout = 10000;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Path", fullPath);
                command.ExecuteNonQuery();
            }
        }
    }
}