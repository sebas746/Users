namespace Users.Core.Entities;

public class User
{
    public long Id { get; set; }
    public required string FirstName { get; set; }
    public string LastName { get; set; } = null!;
    public required string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public long PhoneNumber { get; set; }
}
