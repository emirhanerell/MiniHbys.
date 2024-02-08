using System.ComponentModel.DataAnnotations;

namespace MiniHbys.Entity;

public class User
{
    [Key]
    public int UserID { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public string UserSurname { get; set; }
    public DateTime? BirthDate { get; set; }
    public int RoleID { get; set; }
    public virtual Role? Role { get; set; }
}