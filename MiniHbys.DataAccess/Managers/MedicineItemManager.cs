using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class MedicineItemManager:IMedicineItemManager
{
    public void CreateMedicineItem(MedicineItem medicineItem)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"INSERT INTO MedicineItem (MedicineName,InspectionID,PatientID) VALUES (@MedicineName,
            @InspectionID,@PatientID)";
            using (var command=new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@MedicineName", medicineItem.MedicineName);
                command.Parameters.AddWithValue("@InspectionID",medicineItem.InspectionID);
                command.Parameters.AddWithValue("@PatientID",medicineItem.PatientID);
                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateMedicineItem(MedicineItem medicineItem)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"UPDATE MedicineItem SET MedicineName=@MedicineName,
                        InspectionID=@InspectionID,
                        PatientID=@PatientID
                        WHERE MedicineItemID = @MedicineItemID";
            using (var command=new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@MedicineName", medicineItem.MedicineName);
                command.Parameters.AddWithValue("@InspectionID",medicineItem.InspectionID);
                command.Parameters.AddWithValue("@PatientID",medicineItem.PatientID);
                command.Parameters.AddWithValue("@MedicineItemID",medicineItem.MedicineItemID);
                command.ExecuteNonQuery();
            }
        }
    }

    public void DeleteMedicineItem(int medicineItemId)
    {
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"DELETE FROM MedicineItem WHERE MedicineItemID = @MedicineItemID ";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@MedicineItemID", medicineItemId);
                command.ExecuteNonQuery();
            }
        }
    }

    public MedicineItem GetMedicineItemById(int medicineItemId)
    {
        MedicineItem item = null;

        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM MedicineItem WHERE MedicineItemID = @MedicineItemID";
            using (var command=new SqlCommand(commandText,connection))
            {
                command.Parameters.AddWithValue("@MedicineItemID",medicineItemId);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    item = new MedicineItem();
                    item.MedicineName = reader["MedicineName"] != DBNull.Value
                        ? reader["MedicineName"].ToString()
                        : string.Empty;
                    item.InspectionID = reader["InspectionID"] != DBNull.Value
                        ? reader["InspectionID"].ToInt32()
                        : default;
                    item.PatientID = reader["PatientID"] != DBNull.Value ? reader["PatientID"].ToInt32() : default;
                    item.MedicineItemID = reader["MedicineItemID"].ToInt32();
                    item.Inspection = new InspectionManager().GetInspectionById(item.InspectionID);
                    item.Patient = new PatientManager().GetPatientById(item.PatientID);
                }
            }
        }
        return item;
    }

    public List<MedicineItem> GetAllMedicineItems()
    {
        var medicineItems = new List<MedicineItem>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = @"SELECT * FROM MedicineItem";
            using (var command=new SqlCommand(commandText,connection))
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var item = new MedicineItem();
                    item.MedicineName = reader["MedicineName"] != DBNull.Value
                        ? reader["MedicineName"].ToString()
                        : string.Empty;
                    item.InspectionID = reader["InspectionID"] != DBNull.Value
                        ? reader["InspectionID"].ToInt32()
                        : default;
                    item.PatientID = reader["PatientID"] != DBNull.Value ? reader["PatientID"].ToInt32() : default;
                    item.MedicineItemID = reader["MedicineItemID"].ToInt32();
                    item.Inspection = new InspectionManager().GetInspectionById(item.InspectionID);
                    item.Patient = new PatientManager().GetPatientById(item.PatientID);
                    medicineItems.Add(item);
                }
            }
        }

        return medicineItems;
    }
}