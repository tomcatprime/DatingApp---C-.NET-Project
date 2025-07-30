using API.Entities; // Importing the AppUser entity to use it in the interface
namespace API.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user); // Method to create a token for the given user
    
    
}