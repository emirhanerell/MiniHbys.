using MiniHbys.Business.Abstraction;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Business.Services;

public class ReportService : IReportService
{
    private readonly IReportManager _reportManager;

    public ReportService(IReportManager reportManager)
    {
        _reportManager = reportManager;
    }
    public List<Inspection> GetInspectionsByDate(DateTime startDate, DateTime endDate)
    {
        return _reportManager.InspectionReportByDate(startDate, endDate);
    }

    public List<Inspection> GetInspectionsByDoctor(int doctorId)
    {
        return _reportManager.InspectionReportByDoctor(doctorId);
    }

    public List<Inspection> GetInspectionsByPatient(int patientId)
    {
        return _reportManager.InspectionReportByPatient(patientId);
    }

    public List<MedicineItem> GetMedicineItemsByPatient(int patientId)
    {
        return _reportManager.MedicineItemReportByPatient(patientId);
    }

    public List<Patient> GetPatientsByBirthDate(DateTime startDate, DateTime endDate)
    {
        return _reportManager.PatientReportByBirthDate(startDate, endDate);
    }
}