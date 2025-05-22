using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService) {
      _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request) {
    // TODO: Aggiungi logica registrazione utent
    
    //check password = password confirmation
    if (request.Password != request.ConfirmPassword)
      return BadRequest("Le password non corrispondono.");
          
      var response = _authService.GenerateToken(request.Email);
      return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request) {
      // TODO: Verifica credenziali
      var response = _authService.GenerateToken(request.Email);
      return Ok(response);
    }
}