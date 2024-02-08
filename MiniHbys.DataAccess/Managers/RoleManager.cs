using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class RoleManager : IRoleManager
{
    public void CreateRole(Role role)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"INSERT INTO Role (
                  RoleName,
                  RoleDescription,
                  IsSuperAdmin,
                  DepartmentReadAccess,
                  DepartmentWriteAccess,
                  DoctorReadAccess,
                  DoctorWriteAccess,
                  InspectionReadAccess,
                  InspectionWriteAccess,
                  MedicalOperationReadAccess,
                  MedicalOperationWriteAccess,
                  MedicineItemReadAccess,
                  MedicineItemWriteAccess,
                  PatientReadAccess,
                  PatientWriteAccess) VALUES (@RoleName,
                  @RoleDescription,
                  @IsSuperAdmin,
                  @DepartmentReadAccess,
                  @DepartmentWriteAccess,
                  @DoctorReadAccess,
                  @DoctorWriteAccess,
                  @InspectionReadAccess,
                  @InspectionWriteAccess,
                  @MedicalOperationReadAccess,
                  @MedicalOperationWriteAccess,
                  @MedicineItemReadAccess,
                  @MedicineItemWriteAccess,
                  @PatientReadAccess,
                  @PatientWriteAccess)";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@RoleName", role.RoleName);
                command.Parameters.AddWithValue("@RoleDescription", role.RoleDescription);
                command.Parameters.AddWithValue("@IsSuperAdmin", role.IsSuperAdmin);
                command.Parameters.AddWithValue("@DepartmentReadAccess", role.DepartmentReadAccess);
                command.Parameters.AddWithValue("@DepartmentWriteAccess", role.DepartmentWriteAccess);
                command.Parameters.AddWithValue("@DoctorReadAccess", role.DoctorReadAccess);
                command.Parameters.AddWithValue("@DoctorWriteAccess", role.DoctorWriteAccess);
                command.Parameters.AddWithValue("@InspectionReadAccess", role.InspectionReadAccess);
                command.Parameters.AddWithValue("@InspectionWriteAccess", role.InspectionWriteAccess);
                command.Parameters.AddWithValue("@MedicalOperationReadAccess", role.MedicalOperationReadAccess);
                command.Parameters.AddWithValue("@MedicalOperationWriteAccess", role.MedicalOperationWriteAccess);
                command.Parameters.AddWithValue("@MedicineItemReadAccess", role.MedicineItemReadAccess);
                command.Parameters.AddWithValue("@MedicineItemWriteAccess", role.MedicineItemWriteAccess);
                command.Parameters.AddWithValue("@PatientReadAccess", role.PatientReadAccess);
                command.Parameters.AddWithValue("@PatientWriteAccess", role.PatientWriteAccess);
                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateRole(Role role)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"UPDATE Role SET  RoleName=@RoleName,
                  RoleDescription=@RoleDescription,
                  IsSuperAdmin = @IsSuperAdmin,
                  DepartmentReadAccess=@DepartmentReadAccess,
                  DepartmentWriteAccess=@DepartmentWriteAccess,
                  DoctorReadAccess=@DoctorReadAccess,
                  DoctorWriteAccess=@DoctorWriteAccess,
                  InspectionReadAccess=@InspectionReadAccess,
                  InspectionWriteAccess=@InspectionWriteAccess,
                  MedicalOperationReadAccess=@MedicalOperationReadAccess,
                  MedicalOperationWriteAccess=@MedicalOperationWriteAccess,
                  MedicineItemReadAccess=@MedicineItemReadAccess,
                  MedicineItemWriteAccess=@MedicineItemWriteAccess,
                  PatientReadAccess=@PatientReadAccess,
                  PatientWriteAccess=@PatientWriteAccess
                  WHERE RoleID = @RoleID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@RoleName", role.RoleName);
                command.Parameters.AddWithValue("@RoleDescription", role.RoleDescription);
                command.Parameters.AddWithValue("@IsSuperAdmin", role.IsSuperAdmin);
                command.Parameters.AddWithValue("@RoleID", role.RoleID);
                command.Parameters.AddWithValue("@DepartmentReadAccess", role.DepartmentReadAccess);
                command.Parameters.AddWithValue("@DepartmentWriteAccess", role.DepartmentWriteAccess);
                command.Parameters.AddWithValue("@DoctorReadAccess", role.DoctorReadAccess);
                command.Parameters.AddWithValue("@DoctorWriteAccess", role.DoctorWriteAccess);
                command.Parameters.AddWithValue("@InspectionReadAccess", role.InspectionReadAccess);
                command.Parameters.AddWithValue("@InspectionWriteAccess", role.InspectionWriteAccess);
                command.Parameters.AddWithValue("@MedicalOperationReadAccess", role.MedicalOperationReadAccess);
                command.Parameters.AddWithValue("@MedicalOperationWriteAccess", role.MedicalOperationWriteAccess);
                command.Parameters.AddWithValue("@MedicineItemReadAccess", role.MedicineItemReadAccess);
                command.Parameters.AddWithValue("@MedicineItemWriteAccess", role.MedicineItemWriteAccess);
                command.Parameters.AddWithValue("@PatientReadAccess", role.PatientReadAccess);
                command.Parameters.AddWithValue("@PatientWriteAccess", role.PatientWriteAccess);
                command.ExecuteNonQuery();
            }
        }
    }

    public Role GetRoleById(int roleId)
    {
        Role role = null;
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM Role WHERE RoleID = @RoleID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@RoleID", roleId);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    role = new Role();
                    role.RoleDescription = reader["RoleDescription"] != DBNull.Value
                        ? reader["RoleDescription"]
                            .ToString()
                        : string.Empty;
                    role.RoleName = reader["RoleName"] != DBNull.Value ? reader["RoleName"].ToString() : string.Empty;
                    role.IsSuperAdmin = reader["IsSuperAdmin"] != DBNull.Value
                        ? reader["IsSuperAdmin"].ToBoolean() : false;
                    role.RoleID = reader["RoleID"] != DBNull.Value ? reader["RoleID"].ToInt32() : default;
                    role.DepartmentReadAccess = reader["DepartmentReadAccess"] != DBNull.Value ? reader["DepartmentReadAccess"].ToBoolean() : false;
                    role.DepartmentWriteAccess = reader["DepartmentWriteAccess"] != DBNull.Value ? reader["DepartmentWriteAccess"].ToBoolean() : false;
                    role.DoctorReadAccess = reader["DoctorReadAccess"] != DBNull.Value ? reader["DoctorReadAccess"].ToBoolean() : false;
                    role.DoctorWriteAccess = reader["DoctorWriteAccess"] != DBNull.Value ? reader["DoctorWriteAccess"].ToBoolean() : false;
                    role.InspectionReadAccess = reader["InspectionReadAccess"] != DBNull.Value ? reader["InspectionReadAccess"].ToBoolean() : false;
                    role.InspectionWriteAccess = reader["InspectionWriteAccess"] != DBNull.Value ? reader["InspectionWriteAccess"].ToBoolean() : false;
                    role.PatientReadAccess = reader["PatientReadAccess"] != DBNull.Value ? reader["PatientReadAccess"].ToBoolean() : false;
                    role.PatientWriteAccess = reader["PatientWriteAccess"] != DBNull.Value ? reader["PatientWriteAccess"].ToBoolean() : false;
                    role.MedicalOperationReadAccess = reader["MedicalOperationReadAccess"] != DBNull.Value ? reader["MedicalOperationReadAccess"].ToBoolean() : false;
                    role.MedicalOperationWriteAccess = reader["MedicalOperationWriteAccess"] != DBNull.Value ? reader["MedicalOperationWriteAccess"].ToBoolean() : false;
                    role.MedicineItemReadAccess = reader["MedicineItemReadAccess"] != DBNull.Value ? reader["MedicineItemReadAccess"].ToBoolean() : false;
                    role.MedicineItemWriteAccess = reader["MedicineItemWriteAccess"] != DBNull.Value ? reader["MedicineItemWriteAccess"]
                    .ToBoolean() : false;
                }
            }
        }
        return role;
    }

    public List<Role> GetAllRoles()
    {
        var roles = new List<Role>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM Role";
            using (var command = new SqlCommand(commandText,connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var role = new Role();
                    role.RoleDescription = reader["RoleDescription"] != DBNull.Value
                        ? reader["RoleDescription"]
                            .ToString()
                        : string.Empty;
                    role.RoleName = reader["RoleName"] != DBNull.Value ? reader["RoleName"].ToString() : string.Empty;
                    role.IsSuperAdmin = reader["IsSuperAdmin"] != DBNull.Value
                        ? reader["IsSuperAdmin"].ToBoolean() : false;
                    role.RoleID = reader["RoleID"] != DBNull.Value ? reader["RoleID"].ToInt32() : default;
                    role.DepartmentReadAccess = reader["DepartmentReadAccess"] != DBNull.Value ? reader["DepartmentReadAccess"].ToBoolean() : false;
                    role.DepartmentWriteAccess = reader["DepartmentWriteAccess"] != DBNull.Value ? reader["DepartmentWriteAccess"].ToBoolean() : false;
                    role.DoctorReadAccess = reader["DoctorReadAccess"] != DBNull.Value ? reader["DoctorReadAccess"].ToBoolean() : false;
                    role.DoctorWriteAccess = reader["DoctorWriteAccess"] != DBNull.Value ? reader["DoctorWriteAccess"].ToBoolean() : false;
                    role.InspectionReadAccess = reader["InspectionReadAccess"] != DBNull.Value ? reader["InspectionReadAccess"].ToBoolean() : false;
                    role.InspectionWriteAccess = reader["InspectionWriteAccess"] != DBNull.Value ? reader["InspectionWriteAccess"].ToBoolean() : false;
                    role.PatientReadAccess = reader["PatientReadAccess"] != DBNull.Value ? reader["PatientReadAccess"].ToBoolean() : false;
                    role.PatientWriteAccess = reader["PatientWriteAccess"] != DBNull.Value ? reader["PatientWriteAccess"].ToBoolean() : false;
                    role.MedicalOperationReadAccess = reader["MedicalOperationReadAccess"] != DBNull.Value ? reader["MedicalOperationReadAccess"].ToBoolean() : false;
                    role.MedicalOperationWriteAccess = reader["MedicalOperationWriteAccess"] != DBNull.Value ? reader["MedicalOperationWriteAccess"].ToBoolean() : false;
                    role.MedicineItemReadAccess = reader["MedicineItemReadAccess"] != DBNull.Value ? reader["MedicineItemReadAccess"].ToBoolean() : false;
                    role.MedicineItemWriteAccess = reader["MedicineItemWriteAccess"] != DBNull.Value ? reader["MedicineItemWriteAccess"]
                        .ToBoolean() : false;
                    roles.Add(role);
                }
            }
        }

        return roles;
    }

    public void DeleteRole(int roleId)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"DELETE FROM Role WHERE RoleID = @RoleID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@RoleID", roleId);
                command.ExecuteNonQuery();
            }
        }
    }
}