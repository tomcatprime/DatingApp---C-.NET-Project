namespace API.Entities;

public class AppUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Unique identifier for the user, default to a new GUID, private key in the database

    public required string DisplayName { get; set; } // Display name of the user, typically used in the UI
    
    public required string Email { get; set; }

    public required byte[] PasswordHash { get; set; }

    public required byte[] PasswordSalt { get; set; }


}