using MiniHbys.Entity;

namespace MiniHbys.Business.Abstraction;

public interface IDoctorService
{
    List<Doctor> GetAllDoctors();
    void CreateDoctor(Doctor doctor);
    void UpdateDoctor(Doctor doctor);
    void DeleteDoctor(int doctorId);
    Doctor GetDoctorById(int doctorId);
}