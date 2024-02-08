using MiniHbys.Entity;

namespace MiniHbys.Business.Abstraction;

public interface IMedicineItemService
{
    void CreateMedicineItem(MedicineItem medicineItem);
    List<MedicineItem> GetAllMedicineItems();
    void UpdateMedicineItem(MedicineItem medicineItem);
    MedicineItem GetMedicineItemById(int id);
    void DeleteMedicineItem(int id);
}