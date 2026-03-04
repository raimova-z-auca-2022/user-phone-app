namespace UserPhoneApp.Models;

public class Phone
{
    public int Id { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public int UserId { get; set; }

    public User? User { get; set; }
}
