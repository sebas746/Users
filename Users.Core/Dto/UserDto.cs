namespace Users.Core.Dto;

public record UserDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public DateTime DateOfBirth { get; init; }
    public long PhoneNumber { get; init; }
    public int Age { get; set; }
}
