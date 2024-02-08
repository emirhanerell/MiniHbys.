using MiniHbys.Business.Abstraction;
using MiniHbys.DataAccess.Abstraction;
using MiniHbys.Entity;

namespace MiniHbys.Business.Services;

public class MedicineItemService : IMedicineItemService
{
    private readonly IMedicineItemManager _medicineItemManager;
    public MedicineItemService(IMedicineItemManager medicineItemManager)
    { 
        _medicineItemManager = medicineItemManager;
    }
		
    public void CreateMedicineItem(MedicineItem medicineItem)
    {
        _medicineItemManager.CreateMedicineItem(medicineItem);
    }

    public List<MedicineItem> GetAllMedicineItems()
    {
        return _medicineItemManager.GetAllMedicineItems();
    }
    public void UpdateMedicineItem (MedicineItem medicineItem)
    {
        _medicineItemManager.UpdateMedicineItem(medicineItem);
    }
    public MedicineItem GetMedicineItemById(int id)
    {
        return _medicineItemManager.GetMedicineItemById(id);
    }
    public void DeleteMedicineItem(int id)
    {
        var medicineItem = _medicineItemManager.GetMedicineItemById(id);

        if (medicineItem == null)
            throw new Exception("Couldnt find medicineItem with department id :"+id);
        
        _medicineItemManager.DeleteMedicineItem(id);
    }
}