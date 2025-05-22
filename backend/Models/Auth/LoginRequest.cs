using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required(ErrorMessage = "Email è obbligatorio."),
    EmailAddress(ErrorMessage = "Formato email non valido.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password è obbligatoria."),
    MinLength(8, ErrorMessage = "La password deve contenere almeno 8 caratteri."),
    DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}