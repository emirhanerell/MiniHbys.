using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniHbys.Entity;

namespace MiniHbys.Business.Abstraction
{
	public interface IPatientService
	{
		void CreatePatient(Patient patient);
		List<Patient> GetAllPatients();
		void UpdatePatient(Patient patient);
		Patient GetPatientById(int id);
		void DeletePatient(int id);
    }
}
