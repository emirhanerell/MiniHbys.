using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniHbys.Entity;

namespace MiniHbys.Business.Abstraction
{
    public interface IMedicalOperationService
    {
        List<MedicalOperation> GetAllMedicalOperations();
        void CreateMedicalOperation(MedicalOperation medicalOperation);
        void UpdateMedicalOperation(MedicalOperation medicalOperation);
        void DeleteMedicalOperation(int medicalOperationId);
        MedicalOperation GetMedicalOperationById(int medicalOperationId);
    }
}
