using System.ComponentModel.DataAnnotations;

namespace UserPhoneApp.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(100, ErrorMessage = "Имя слишком длинное")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [StringLength(200, ErrorMessage = "Email слишком длинный")]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Дата рождения")]
    public DateTime? DateOfBirth { get; set; }

    public List<Phone> Phones { get; set; } = new();
}