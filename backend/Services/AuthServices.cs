using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{
    private readonly IConfiguration _config;
    private readonly AppDbContext _dbContext;

    public AuthService(IConfiguration config, AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _config = config;
    }

    public AuthResponse GenerateToken(string email){
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds
        );

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = token.ValidTo
        };
    }

    public AuthResponse Register(RegisterRequest request)
    {
        // Verifica se l'email esiste già
        if (_dbContext.Users.Any(u => u.Email == request.Email))
            throw new Exception("Email già registrata");

        // Hash password
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Email = request.Email,
            PasswordHash = hashedPassword
        };

        _dbContext.Users.Add(user);
        var newUser = _dbContext.SaveChanges();

        return GenerateToken(user.Email);
    }

    public AuthResponse Login(LoginRequest request)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new Exception("Credenziali non valide");

        return GenerateToken(user.Email);
    }
    
}