using MiniHbys.Entity;

namespace MiniHbys.DataAccess.Abstraction;

public interface IInspectionManager
{
    void CreateInspection(Inspection inspection);
    void UpdateInspection(Inspection inspection);
    void DeleteInspection(int inspectionId);
    Inspection GetInspectionById(int inspectionId);
    List<Inspection> GetAllInspections();
}