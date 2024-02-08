using MiniHbys.Business.Abstraction;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Business.Services;

public class DoctorService:IDoctorService
{
    private readonly IDoctorManager _doctorManager;
    public DoctorService(IDoctorManager doctorManager)
    {
        _doctorManager = doctorManager;
    }
    
    public List<Doctor> GetAllDoctors()
    {
        return _doctorManager.GetAllDoctors();
    }

    public void CreateDoctor(Doctor doctor)
    {
        _doctorManager.CreateDoctor(doctor);
    }
    public void UpdateDoctor(Doctor doctor)
    {
        _doctorManager.UpdateDoctor(doctor);
    }

    public void DeleteDoctor(int doctorId)
    {
        var doctor = _doctorManager.GetDoctorById(doctorId);

        if (doctor == null)
            throw new Exception("Couldnt find department with department id :"+doctorId);
        
        _doctorManager.DeleteDoctor(doctorId);
    }
    public Doctor GetDoctorById(int doctorId)
    {
        return _doctorManager.GetDoctorById(doctorId);
    }
}