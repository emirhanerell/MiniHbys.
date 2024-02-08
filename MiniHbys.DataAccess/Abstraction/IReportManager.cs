using MiniHbys.Entity;

namespace MiniHbys.DataAccess.Abstraction;

public interface IReportManager
{
    List<Inspection> InspectionReportByDate(DateTime startDate, DateTime endDate);
    List<Inspection> InspectionReportByDoctor(int doctorId);
    List<Inspection> InspectionReportByPatient(int patientId);
    List<MedicineItem> MedicineItemReportByPatient(int patientId);
    List<Patient> PatientReportByBirthDate(DateTime startDate, DateTime endDate);
}