using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class DoctorManager : IDoctorManager
{
    public void CreateDoctor(Doctor doctor)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"INSERT INTO Doctor (DoctorName,DoctorSurname,DepartmentID) VALUES(@DoctorName,
            @DoctorSurname,@DepartmentID)";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@DoctorName", doctor.DoctorName);
                command.Parameters.AddWithValue("@DoctorSurname", doctor.DoctorSurname);
                command.Parameters.AddWithValue("@DepartmentID", doctor.DepartmentID);

                command.ExecuteNonQuery();
            }
        }
    }

    public List<Doctor> GetAllDoctors()
    {
        var doctors = new List<Doctor>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM Doctor";
            using (var command= new SqlCommand(commandText,connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var doctor = new Doctor();
                    doctor.DoctorName = reader["DoctorName"] != DBNull.Value
                        ? reader["DoctorName"].ToString()
                        : string.Empty;
                    doctor.DoctorSurname = reader["DoctorSurname"] != DBNull.Value
                        ? reader["DoctorSurname"].ToString
                            ()
                        : string.Empty;
                    doctor.DepartmentID = reader["DepartmentID"] != DBNull.Value
                        ? reader["DepartmentID"].ToInt32()
                        : default;
                    doctor.DoctorID = reader["DoctorID"].ToInt32();
                    doctor.Department = new DepartmentManager().GetDepartmentById(doctor.DepartmentID);
                    doctors.Add(doctor);
                }
            }
        }
        return doctors;
    }

    public Doctor GetDoctorById(int doctorId)   
    {
        Doctor doctor = null;
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM Doctor WHERE DoctorID = @DoctorID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@DoctorID", doctorId);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    doctor = new Doctor();
                    doctor.DoctorName = reader["DoctorName"] != DBNull.Value
                        ? reader["DoctorName"].ToString()
                        : string.Empty;
                    doctor.DoctorSurname = reader["DoctorSurname"] != DBNull.Value
                        ? reader["DoctorSurname"].ToString
                            ()
                        : string.Empty;
                    doctor.DepartmentID = reader["DepartmentID"] != DBNull.Value
                        ? reader["DepartmentID"].ToInt32()
                        : default;
                    doctor.DoctorID = reader["DoctorID"].ToInt32();
                    doctor.Department = new DepartmentManager().GetDepartmentById(doctor.DepartmentID);
                }
            }
        }

        return doctor;
    }

    public void DeleteDoctor(int doctorId)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"DELETE FROM Doctor WHERE DoctorID = @DoctorID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@DoctorID", doctorId);
                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateDoctor(Doctor doctor)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"UPDATE Doctor SET DoctorName = @DoctorName,
                                DoctorSurname = @DoctorSurname,
                                DepartmentID = @DepartmentID
                                WHERE DoctorID = @DoctorID";

            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@DoctorSurname", doctor.DoctorSurname);
                command.Parameters.AddWithValue("@DoctorName", doctor.DoctorName);
                command.Parameters.AddWithValue("@DepartmentID", doctor.DepartmentID);
                command.Parameters.AddWithValue("@DoctorID", doctor.DoctorID);

                command.ExecuteNonQuery();
            }
        }
    }
}