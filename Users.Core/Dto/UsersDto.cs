namespace Users.Core.Dto;

public record UsersDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public DateTime DateOfBirth { get; init; }
    public long PhoneNumber { get; init; }
}
