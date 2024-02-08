using System.ComponentModel.DataAnnotations;

namespace MiniHbys.Entity;

public class Inspection
{
    [Key]
    public int InspectionID { get; set; }
    public DateTime? InspectionDate { get; set; }
    public int DoctorID { get; set; }
    public int PatientID { get; set; }
    public string InspectionResult { get; set; }
    public virtual Patient? Patient { get; set; }
    public virtual Doctor? Doctor { get; set; }
}