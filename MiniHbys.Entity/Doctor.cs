using System.ComponentModel.DataAnnotations;

namespace MiniHbys.Entity;

public class Doctor
{
    [Key]
    public int DoctorID { get; set; }
    public string DoctorName { get; set; }
    public string DoctorSurname { get; set; }
    public int DepartmentID { get; set; }
    public virtual Department? Department { get; set; }
}