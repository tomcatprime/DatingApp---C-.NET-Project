using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService // Implementation of the ITokenService interface
{
    public string CreateToken(AppUser user) // Method to create a token for the given user
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot get token key"); //this is called coalescing operator, it will throw an exception if the token key is not found in the configuration
        // Retrieve the token key from the configuration
        if (tokenKey.Length < 64) throw new Exception("TokenKey is needs to be => 64 characters long"); // Ensure the token key is at least 64 characters long
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)); // Create a symmetric security key from the token key
        
        //adding information to the token, adding claims
        
        var claims = new List<Claim> //
        {
            new Claim(ClaimTypes.Email, user.Email), //New claim Email
            new(ClaimTypes.NameIdentifier, user.Id) //New claim Name
        };
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); // Signing Credentials to token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), //
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}