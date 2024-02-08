using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniHbys.Business.Abstraction;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.DataAccess.Managers;
using MiniHbys.Entity;
using MiniHbys.Utilities;

namespace MiniHbys.Business.Services
{
	public class PatientService : IPatientService
	{
		private readonly IPatientManager _patientManager;
		public PatientService(IPatientManager patientmanager)
		{ 
			_patientManager = patientmanager;
		}
		
		public void CreatePatient(Patient patient)
		{
			_patientManager.CreatePatient(patient);
		}

        public List<Patient> GetAllPatients()
        {
            return _patientManager.GetAllPatients();
        }
		public void UpdatePatient(Patient patient)
		{
			_patientManager.UpdatePatient(patient);
		}
		public Patient GetPatientById(int id)
		{
			return _patientManager.GetPatientById(id);
		}
        public void DeletePatient(int id)
        {
	        var patient = _patientManager.GetPatientById(id);

	        if (patient == null)
		        throw new Exception("Couldnt find patient with department id :"+id);
        
	        _patientManager.DeletePatient(id);
        }
    }
}
