namespace MiniHbys.Common.WebViewModels;

public class RegisterViewModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
    public DateTime Birthdate { get; set; }
    public int RoleId { get; set; }
}