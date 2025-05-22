using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [Required(ErrorMessage = "Email è obbligatorio."),
    EmailAddress(ErrorMessage = "Formato email non valido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password è obbligatoria."),
    MinLength(8, ErrorMessage = "La password deve contenere almeno 8 caratteri.")]
    public string Password { get; set; }
}