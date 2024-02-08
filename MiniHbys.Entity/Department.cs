using System.ComponentModel.DataAnnotations;

namespace MiniHbys.Entity;

public class Department
{
    [Key]
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; }
}