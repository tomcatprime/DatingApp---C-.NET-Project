using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography; // For hashing passwords

namespace API.Controllers
{
    public class AccountController(AppDbContext context) : BaseApiController
    {

        [HttpPost("register")] // Endpoint to register a new user, api/account/register
        public async Task<ActionResult<AppUser>> Register(string email, string displayName, string password)
        {
            using var hmac = new HMACSHA512(); // Create a new instance of HMACSHA512 for hashing passwords, word "using" ensures proper disposal of the object, freeing resources after use
            var user = new AppUser
            {
                DisplayName = displayName,
                Email = email,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)), // Hash the password using HMACSHA512
                PasswordSalt = hmac.Key // Store the key used for hashing as the password salt
            };

            context.Users.Add(user); // Add the new user to the database context
            await context.SaveChangesAsync(); // Save changes to the database asynchronously

            return Ok(user); // Return the created user with a 200 OK status
        }

    }
}
