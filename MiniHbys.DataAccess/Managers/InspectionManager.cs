using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class InspectionManager:IInspectionManager
{
    public void CreateInspection(Inspection inspection)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"INSERT INTO Inspection (InspectionDate,DoctorID,PatientID,InspectionResult) VALUES 
            (@InspectionDate,@DoctorID,@PatientID,@InspectionResult)";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@InspectionDate", inspection.InspectionDate);
                command.Parameters.AddWithValue("@DoctorID", inspection.DoctorID);
                command.Parameters.AddWithValue("@PatientID", inspection.PatientID);
                command.Parameters.AddWithValue("@InspectionResult", inspection.InspectionResult);

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateInspection(Inspection inspection)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"UPDATE Inspection SET InspectionDate = @InspectionDate,
                      DoctorID = @DoctorID,
                      PatientID = @PatientID,
                      InspectionResult = @InspectionResult
                      WHERE InspectionID = @InspectionID";
            using (var command = new SqlCommand( commandText,connection))
            {
                command.Parameters.AddWithValue("@InspectionDate", inspection.InspectionDate);
                command.Parameters.AddWithValue("@DoctorID", inspection.DoctorID);
                command.Parameters.AddWithValue("@PatientID", inspection.PatientID);
                command.Parameters.AddWithValue("@InspectionResult", inspection.InspectionResult);
                command.Parameters.AddWithValue("@InspectionID", inspection.InspectionID);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteInspection(int inspectionId)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"DELETE FROM Inspection WHERE InspectionID = @InspectionID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@InspectionID", inspectionId);
                command.ExecuteNonQuery();
            }
        }
    }

    public Inspection GetInspectionById(int inspectionId)
    {
        Inspection inspection = null;
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM Inspection WHERE InspectionID = @InspectionID ";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@InspectionID", inspectionId);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    inspection = new Inspection();
                    inspection.InspectionDate = reader["InspectionDate"] != DBNull.Value
                        ? reader["InspectionDate"]
                            .ToDateTime()
                        : null;
                    inspection.InspectionResult = reader["InspectionResult"] != DBNull.Value
                        ? reader["InspectionResult"].ToString()
                        : string.Empty;
                    inspection.DoctorID = reader["DoctorID"] != DBNull.Value ? reader["DoctorID"].ToInt32() : default;
                    inspection.InspectionID = reader["InspectionID"] != DBNull.Value ? reader["InspectionID"].ToInt32() : 
                    default;
                    inspection.PatientID =
                        reader["PatientID"] != DBNull.Value ? reader["PatientID"].ToInt32() : default;
                    inspection.Doctor = new DoctorManager().GetDoctorById(inspection.DoctorID);
                    inspection.Patient = new PatientManager().GetPatientById(inspection.PatientID);
                }
            }
        }
        return inspection;
    }

    public List<Inspection> GetAllInspections()
    {
        var inspections = new List<Inspection>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = "SELECT * FROM Inspection";
            using (var command = new SqlCommand(commandText,connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var inspection = new Inspection();
                    inspection.InspectionDate = reader["InspectionDate"] != DBNull.Value
                        ? reader["InspectionDate"]
                            .ToDateTime()
                        : null;
                    inspection.InspectionResult = reader["InspectionResult"] != DBNull.Value
                        ? reader["InspectionResult"].ToString()
                        : string.Empty;
                    inspection.DoctorID = reader["DoctorID"] != DBNull.Value ? reader["DoctorID"].ToInt32() : default;
                    inspection.InspectionID = reader["InspectionID"] != DBNull.Value ? reader["InspectionID"].ToInt32() : 
                        default;
                    inspection.PatientID =
                        reader["PatientID"] != DBNull.Value ? reader["PatientID"].ToInt32() : default;
                    inspection.Doctor = new DoctorManager().GetDoctorById(inspection.DoctorID);
                    inspection.Patient = new PatientManager().GetPatientById(inspection.PatientID);
                    inspections.Add(inspection);
                }
            }
        }
        return inspections;
    }
}