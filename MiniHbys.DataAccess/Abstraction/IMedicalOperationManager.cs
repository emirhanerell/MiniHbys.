using MiniHbys.Entity;

namespace MiniHbys.DataAccess.Abstraction;

public interface IMedicalOperationManager
{
    void CreateMedicalOperation(MedicalOperation medicalOperation);
    void UpdateMedicalOperation(MedicalOperation medicalOperation);
    void DeleteMedicalOperation(int medicalOperationId);
    MedicalOperation GetMedicalOperationById(int medicalOperationId);
    List<MedicalOperation> GetAllMedicalOperations();
}