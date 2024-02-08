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
    public class MedicalOperationService:IMedicalOperationService
    {
        private readonly IMedicalOperationManager _medicalOperationManager;

        public MedicalOperationService(IMedicalOperationManager medicalOperationManager)
        {
            _medicalOperationManager = medicalOperationManager;
        }
        public List<MedicalOperation> GetAllMedicalOperations()
        {
            return _medicalOperationManager.GetAllMedicalOperations();
        }

        public void CreateMedicalOperation(MedicalOperation medicalOperation)
        {
            _medicalOperationManager.CreateMedicalOperation(medicalOperation);
        }
        public void UpdateMedicalOperation(MedicalOperation medicalOperation)
        {
            _medicalOperationManager.UpdateMedicalOperation(medicalOperation);
        }

        public void DeleteMedicalOperation(int medicalOperationId)
        {
            var medicalOperation = _medicalOperationManager.GetMedicalOperationById(medicalOperationId);

            if (medicalOperation == null)
                throw new Exception("Couldnt find medical operation with department id :"+medicalOperationId);
        
            _medicalOperationManager.DeleteMedicalOperation(medicalOperationId);
        }
        public MedicalOperation GetMedicalOperationById(int medicalOperationId)
        {
            return _medicalOperationManager.GetMedicalOperationById(medicalOperationId);
        }
    }
}
