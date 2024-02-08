using System.Data;
using Microsoft.Data.SqlClient;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.DataAccess.Managers;

public class ReportManager : IReportManager
{
    public List<Inspection> InspectionReportByDate(DateTime startDate,DateTime endDate)
    {
        List<Inspection> inspections = new List<Inspection>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = "sp_InspectionReportByDate";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StartDate",startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

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

    public List<Inspection> InspectionReportByDoctor(int doctorId)
    {
        List<Inspection> inspections = new List<Inspection>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = "sp_InspectionReportByDoctor";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DoctorId",doctorId);
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

    public List<Inspection> InspectionReportByPatient(int patientId)
    {
        List<Inspection> inspections = new List<Inspection>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = "sp_InspectionReportByPatient";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PatientId",patientId);

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

    public List<MedicineItem> MedicineItemReportByPatient(int patientId)
    {
        var medicineItems = new List<MedicineItem>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = "sp_MedicineItemReportByPatient";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PatientId", patientId);
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

    public List<Patient> PatientReportByBirthDate(DateTime startDate, DateTime endDate)
    {
        var patients = new List<Patient>();
        using (var connection = new SqlConnection(GlobalSettings.ConnectionString))
        {
            connection.Open();
            var commandText = "sp_PatientReportByBirthDate";
            using (var command = new SqlCommand(commandText,connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StartDate",startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

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