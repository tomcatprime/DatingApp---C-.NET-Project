namespace API.DTOs;

public class UserDto
{
    public required string Id { get; set; } //
    
    public required string Email { get; set; }
    
    public required string DisplayName { get; set; } // Username
    
    public string? ImageUrl { get; set; } // OPTIONAL

    public required string Token { get; set; }
    
}