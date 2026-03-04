namespace UserPhoneApp.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }
    
    public List<Phone> Phones { get; set; } = new();
}