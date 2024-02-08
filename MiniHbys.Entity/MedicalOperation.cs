using System.ComponentModel.DataAnnotations;

namespace MiniHbys.Entity;

public class MedicalOperation
{
    [Key]
    public int MedicalOperationID { get; set; }
    public int InspectionID { get; set; }
    public DateTime? MedicalOperationDate { get; set; }
    public string Notes { get; set; }
    public virtual Inspection? Inspection { get; set; }
}