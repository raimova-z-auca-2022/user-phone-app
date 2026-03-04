using System.ComponentModel.DataAnnotations;
using UserPhoneApp.Validation;

namespace UserPhoneApp.Models;

public class Phone
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Номер телефона обязателен")]
    [StringLength(30, ErrorMessage = "Номер слишком длинный")]
    [DigitsOnly(ErrorMessage = "Номер должен содержать только цифры")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Выберите пользователя")]
    public int UserId { get; set; }

    public User? User { get; set; }
}
