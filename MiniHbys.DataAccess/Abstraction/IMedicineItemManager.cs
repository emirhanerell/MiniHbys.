using MiniHbys.Entity;

namespace MiniHbys.DataAccess.Abstraction;

public interface IMedicineItemManager
{
    void CreateMedicineItem(MedicineItem medicineItem);
    void UpdateMedicineItem(MedicineItem medicineItem);
    void DeleteMedicineItem(int medicineItemId);
    MedicineItem GetMedicineItemById(int medicineItemId);
    List<MedicineItem> GetAllMedicineItems();
}