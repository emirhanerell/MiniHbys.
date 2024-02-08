using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class MedicalOperationManager : IMedicalOperationManager
{
    public void CreateMedicalOperation(MedicalOperation medicalOperation)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"INSERT INTO MedicalOperation (InspectionID,MedicalOperationDate,Notes) VALUES 
            (@InspectionID,@MedicalOperationDate,@Notes)";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@InspectionID",medicalOperation.InspectionID);
                command.Parameters.AddWithValue("@MedicalOperationDate",medicalOperation.MedicalOperationDate);
                command.Parameters.AddWithValue("@Notes", medicalOperation.Notes);

                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateMedicalOperation(MedicalOperation medicalOperation)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"UPDATE MedicalOperation SET InspectionID=@InspectionID,MedicalOperationDate = @MedicalOperationDate,Notes = @Notes WHERE MedicalOperationID = @MedicalOperationID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@InspectionID",medicalOperation.InspectionID);
                command.Parameters.AddWithValue("@MedicalOperationDate",medicalOperation.MedicalOperationDate);
                command.Parameters.AddWithValue("@Notes", medicalOperation.Notes);
                command.Parameters.AddWithValue("@MedicalOperationID", medicalOperation.MedicalOperationID);

                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteMedicalOperation(int medicalOperationId)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"DELETE FROM MedicalOperation WHERE MedicalOperationID = @MedicalOperationID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@MedicalOperationID", medicalOperationId);
                command.ExecuteNonQuery();
            }
        }
    }

    public MedicalOperation GetMedicalOperationById(int medicalOperationId)
    {
        MedicalOperation medicalOperation = null;
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM MedicalOperation WHERE MedicalOperationID = @MedicalOperationID";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@MedicalOperationID", medicalOperationId);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    medicalOperation = new MedicalOperation();
                    medicalOperation.Notes =
                        reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : string.Empty;
                    medicalOperation.InspectionID = reader["InspectionID"] != DBNull.Value
                        ? reader["InspectionID"]
                            .ToInt32()
                        : default;
                    medicalOperation.MedicalOperationDate = reader["MedicalOperationDate"] != DBNull.Value
                        ? reader["MedicalOperationDate"].ToDateTime()
                        : null;
                    medicalOperation.MedicalOperationID = reader["MedicalOperationID"] != DBNull.Value
                        ? reader["MedicalOperationID"].ToInt32()
                        : default;
                    medicalOperation.Inspection =
                        new InspectionManager().GetInspectionById(medicalOperation.InspectionID);
                }
            }
        }
        return medicalOperation;
    }

    public List<MedicalOperation> GetAllMedicalOperations()
    {
        var medicalOperations = new List<MedicalOperation>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM MedicalOperation";
            using (var command = new SqlCommand(commandText,connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var medicalOperation = new MedicalOperation();
                    medicalOperation.Notes =
                        reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : string.Empty;
                    medicalOperation.InspectionID = reader["InspectionID"] != DBNull.Value
                        ? reader["InspectionID"]
                            .ToInt32()
                        : default;
                    medicalOperation.MedicalOperationDate = reader["MedicalOperationDate"] != DBNull.Value
                        ? reader["MedicalOperationDate"].ToDateTime()
                        : null;
                    medicalOperation.MedicalOperationID = reader["MedicalOperationID"] != DBNull.Value
                        ? reader["MedicalOperationID"].ToInt32()
                        : default;
                    medicalOperation.Inspection =
                        new InspectionManager().GetInspectionById(medicalOperation.InspectionID);
                    medicalOperations.Add(medicalOperation);
                }
            }
        }
        return medicalOperations;
    }
}