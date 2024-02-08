using MiniHbys.Entity;

namespace MiniHbys.Business.Abstraction;

public interface IReportService
{
    List<Inspection> GetInspectionsByDate(DateTime startDate, DateTime endDate);
    List<Inspection> GetInspectionsByDoctor(int doctorId);
    List<Inspection> GetInspectionsByPatient(int patientId);
    List<MedicineItem> GetMedicineItemsByPatient(int patientId);
    List<Patient> GetPatientsByBirthDate(DateTime startDate, DateTime endDate);
}