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
      try
      {
        var response = _authService.Register(request);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true, // Solo in produzione con HTTPS
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(3)
        };
        Response.Cookies.Append("auth-token", response.Token, cookieOptions);
        return Ok(response);
      }
      catch (Exception ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request) {
      try
      {
          var response = _authService.Login(request);
          
          // Stesso codice per i cookie:
          var cookieOptions = new CookieOptions
          {
              HttpOnly = true,
              Secure = true,
              SameSite = SameSiteMode.Strict,
              Expires = DateTime.UtcNow.AddHours(3)
          };
          
          Response.Cookies.Append("auth-token", response.Token, cookieOptions);
          
          return Ok(response);
      }
      catch (Exception ex)
      {
          return BadRequest(new { message = ex.Message });
      }
    }
}