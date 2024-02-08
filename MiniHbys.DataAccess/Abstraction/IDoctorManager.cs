using MiniHbys.Entity;

namespace MiniHbys.DataAccess.Abstraction;

public interface IDoctorManager
{
    void CreateDoctor(Doctor doctor);
    List<Doctor> GetAllDoctors();
    Doctor GetDoctorById(int doctorId);
    void DeleteDoctor(int doctorId);
    void UpdateDoctor(Doctor doctor);
}