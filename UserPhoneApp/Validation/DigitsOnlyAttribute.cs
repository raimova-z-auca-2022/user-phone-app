using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserPhoneApp.Validation;

public sealed class DigitsOnlyAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var s = value as string;

        if (string.IsNullOrWhiteSpace(s))
            return ValidationResult.Success;

        return Regex.IsMatch(s, @"^\d+$")
            ? ValidationResult.Success
            : new ValidationResult(ErrorMessage ?? "Номер должен содержать только цифры");
    }
}