using System.ComponentModel.DataAnnotations;

namespace MiniHbys.Entity;

public class Role
{
    [Key]
    public int RoleID { get; set; }
    public string RoleName { get; set; }
    public string RoleDescription { get; set; }
    public bool IsSuperAdmin { get; set; }
    public bool DepartmentReadAccess { get; set; }
    public bool DepartmentWriteAccess { get; set; }
    public bool DoctorReadAccess { get; set; }
    public bool DoctorWriteAccess { get; set; }
    public bool InspectionReadAccess { get; set; }
    public bool InspectionWriteAccess { get; set; }
    public bool MedicalOperationReadAccess { get; set; }
    public bool MedicalOperationWriteAccess { get; set; }
    public bool MedicineItemReadAccess { get; set; }
    public bool MedicineItemWriteAccess { get; set; }
    public bool PatientReadAccess { get; set; }
    public bool PatientWriteAccess { get; set; }
}