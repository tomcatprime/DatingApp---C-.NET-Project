using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography; // For hashing passwords

namespace API.Controllers
{
    public class AccountController(AppDbContext context) : BaseApiController
    {

        [HttpPost("register")] // Endpoint to register a new user, api/account/register
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto) // RegisterDto contains the required data for registration

        {

            if(await EmailExists(registerDto.Email)) return BadRequest("Email is already in use"); // Check if the email already exists, return BadRequest if it does
            //we need to use await to ensure that the asynchronous operation completes before proceeding, this is important to avoid blocking the thread and to ensure that the application remains responsive



            using var hmac = new HMACSHA512(); // Create a new instance of HMACSHA512 for hashing passwords, word "using" ensures proper disposal of the object, freeing resources after use
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password)), // Hash the password using HMACSHA512
                PasswordSalt = hmac.Key // Store the key used for hashing as the password salt
            };

            context.Users.Add(user); // Add the new user to the database context
            await context.SaveChangesAsync(); // Save changes to the database asynchronously

            return Ok(user); // Return the created user with a 200 OK status
        }

        private async Task<bool> EmailExists(string email) // Check if the email already exists in the database
        {
            return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower()); // Asynchronously check if any user has the specified email

        }
    }
}
