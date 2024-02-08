using System.ComponentModel.DataAnnotations;

namespace MiniHbys.Entity;

public class Patient
{
    [Key]
    public int PatientID { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PatientGender { get; set; }
    public string BloodGroup { get; set; }
    public DateTime? BirthDate { get; set; }
}