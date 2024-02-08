using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class DepartmentManager : IDepartmentManager
{
    public void CreateDepartment(Department department)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"INSERT INTO Department(DepartmentName) VALUES(@DepartmentName)";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                command.ExecuteNonQuery(); 
            }
        }
    }

    public void UpdateDepartment(Department department)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"UPDATE Department SET DepartmentName = @DepartmentName WHERE DepartmentID = 
            @DepartmentID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                command.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteDepartment(int departmentId)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"DELETE FROM Department WHERE DepartmentID = @DepartmentID";
            using (var command=new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@DepartmentID", departmentId);
                command.ExecuteNonQuery();
            }
        }
    }

    public Department GetDepartmentById(int departmentId)   
    {
        Department department = null;
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM Department WHERE DepartmentID = @DepartmentID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@DepartmentID", departmentId); 
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    department = new Department();
                    department.DepartmentName = reader["DepartmentName"] != DBNull.Value
                        ? reader["DepartmentName"]
                            .ToString()
                        : string.Empty;
                    department.DepartmentID = reader["DepartmentID"].ToInt32();
                }
            }
        }

        return department;
    }

    public List<Department> GetAllDepartments()
    {
        var departments = new List<Department>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = "SELECT * FROM Department";
            using (var command = new SqlCommand( commandText,connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var department = new Department();
                    department.DepartmentName = reader["DepartmentName"] != DBNull.Value
                        ? reader["DepartmentName"]
                            .ToString()
                        : string.Empty;
                    department.DepartmentID = reader["DepartmentID"].ToInt32();
                    departments.Add(department);
                }
            }
        }

        return departments;
    }
}