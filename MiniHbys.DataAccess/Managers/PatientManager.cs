using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class PatientManager : IPatientManager
{
    public void CreatePatient(Patient patient)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"INSERT INTO Patient (PatientName,PatientSurname,PatientGender,BloodGroup,BirthDate) 
            VALUES (@PatientName,@PatientSurname,@PatientGender,@BloodGroup,@BirthDate)";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@PatientName", patient.PatientName);
                command.Parameters.AddWithValue("@PatientSurname", patient.PatientSurname);
                command.Parameters.AddWithValue("@PatientGender", patient.PatientGender);
                command.Parameters.AddWithValue("@BloodGroup", patient.BloodGroup);
                command.Parameters.AddWithValue("@BirthDate", patient.BirthDate);
                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdatePatient(Patient patient)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"UPDATE Patient SET PatientName = @PatientName,
                   PatientSurname = @PatientSurname,
                   PatientGender = @PatientGender,
                   BloodGroup = @BloodGroup,
                   BirthDate = @BirthDate
                   WHERE PatientID = @PatientID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@PatientName", patient.PatientName);
                command.Parameters.AddWithValue("@PatientSurname", patient.PatientSurname);
                command.Parameters.AddWithValue("@PatientGender", patient.PatientGender);
                command.Parameters.AddWithValue("@BloodGroup", patient.BloodGroup);
                command.Parameters.AddWithValue("@BirthDate", patient.BirthDate);
                command.Parameters.AddWithValue("@PatientID", patient.PatientID);

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeletePatient(int patientId)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"DELETE FROM Patient WHERE PatientID = @PatientID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@PatientID", patientId);
                command.ExecuteNonQuery();
            }
        }
    }

    public Patient GetPatientById(int patientId)
    {
        Patient patient = null;
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM Patient WHERE PatientID = @PatientID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@PatientID", patientId);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    patient = new Patient();
                    patient.BirthDate = reader["BirthDate"] != DBNull.Value ? reader["BirthDate"].ToDateTime() : null;
                    patient.BloodGroup =
                        reader["BloodGroup"] != DBNull.Value ? reader["BloodGroup"].ToString() : string.Empty;
                    patient.PatientGender = reader["PatientGender"] != DBNull.Value
                        ? reader["PatientGender"].ToString
                            ()
                        : string.Empty;
                    patient.PatientName = reader["PatientName"] != DBNull.Value
                        ? reader["PatientName"].ToString()
                        : string.Empty;
                    patient.PatientSurname = reader["PatientSurname"] != DBNull.Value
                        ? reader["PatientSurname"].ToString()
                        : string.Empty;
                    patient.PatientID = reader["PatientID"].ToInt32();
                }
            }
        }
        return patient;
    }

    public List<Patient> GetAllPatients()
    {
        var patients = new List<Patient>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM Patient";
            using (var command = new SqlCommand(commandText,connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    
                    var patient = new Patient();
                    patient.BirthDate = reader["BirthDate"] != DBNull.Value ? reader["BirthDate"].ToDateTime() : null;
                    patient.BloodGroup =
                        reader["BloodGroup"] != DBNull.Value ? reader["BloodGroup"].ToString() : string.Empty;
                    patient.PatientGender = reader["PatientGender"] != DBNull.Value
                        ? reader["PatientGender"].ToString
                            ()
                        : string.Empty;
                    patient.PatientName = reader["PatientName"] != DBNull.Value
                        ? reader["PatientName"].ToString()
                        : string.Empty;
                    patient.PatientSurname = reader["PatientSurname"] != DBNull.Value
                        ? reader["PatientSurname"].ToString()
                        : string.Empty;
                    patient.PatientID = reader["PatientID"].ToInt32();
                    patients.Add(patient);
                }
            }
        }

        return patients;
    }
}