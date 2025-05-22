using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
  [Required(ErrorMessage = "Email obbligatoria")]
    [EmailAddress(ErrorMessage = "Formato email non valido")]
    [MaxLength(100, ErrorMessage = "Lunghezza massima 100 caratteri")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password obbligatoria")]
    [MinLength(8, ErrorMessage = "Minimo 8 caratteri")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Conferma password obbligatoria")]
    [Compare(nameof(Password), ErrorMessage = "Le password non coincidono")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
}