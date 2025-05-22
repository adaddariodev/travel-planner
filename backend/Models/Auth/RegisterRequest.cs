using System.ComponentModel.DataAnnotations;

public class RegisterRequest : LoginRequest
{
  [Required, Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}