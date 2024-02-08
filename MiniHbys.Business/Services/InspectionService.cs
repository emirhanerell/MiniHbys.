using MiniHbys.Business.Abstraction;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Business.Services;

public class InspectionService :IInspectionService
{
    private readonly IInspectionManager _inspectionManager;
    public InspectionService(IInspectionManager inspectionManager)
    {
        _inspectionManager = inspectionManager;
    }
    
    public List<Inspection> GetAllInspections()
    {
        return _inspectionManager.GetAllInspections();
    }

    public void CreateInspection(Inspection inspection)
    {
        _inspectionManager.CreateInspection(inspection);
    }
    public void UpdateInspection(Inspection inspection)
    {
        _inspectionManager.UpdateInspection(inspection);
    }

    public void DeleteInspection(int inspectionId)
    {
        var inspection = _inspectionManager.GetInspectionById(inspectionId);
        if (inspection == null)
            throw new Exception("Couldnt find inspection with department id :"+inspectionId);
        
        _inspectionManager.DeleteInspection(inspectionId);
    }
    public Inspection GetInspectionById(int inspectionId)
    {
        return _inspectionManager.GetInspectionById(inspectionId);
    }
}