using System.ComponentModel.DataAnnotations;

namespace MiniHbys.Entity;

public class MedicineItem
{
    [Key]
    public int MedicineItemID { get; set; }
    public string MedicineName { get; set; }
    public int InspectionID { get; set; }
    public int PatientID { get; set; }
    public virtual Patient? Patient { get; set; }
    public virtual Inspection? Inspection { get; set; }
}