using MiniHbys.Entity;

namespace MiniHbys.DataAccess.Abstraction;

public interface IPatientManager
{
    void CreatePatient(Patient patient);
    void UpdatePatient(Patient patient);
    void DeletePatient(int patientId);
    Patient GetPatientById(int patientId);
    List<Patient> GetAllPatients();
}