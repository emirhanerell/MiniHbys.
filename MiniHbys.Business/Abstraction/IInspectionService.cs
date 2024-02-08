using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Business.Abstraction;

public interface IInspectionService
{
    List<Inspection> GetAllInspections();
    void CreateInspection(Inspection inspection);
    void UpdateInspection(Inspection inspection);
    void DeleteInspection(int inspectionId);
    Inspection GetInspectionById(int inspectionId);
}