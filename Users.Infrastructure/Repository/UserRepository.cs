using Microsoft.Data.SqlClient;
using Users.Core.Entities;
using Users.Core.Interfaces;

namespace Users.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;
    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<long> AddAsync(User user)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("INSERT INTO Users (FirstName, LastName, Email, DateOfBirth, PhoneNumber) " +
                "VALUES (@FirstName, @LastName, @Email, @DateOfBirth, @PhoneNumber);", connection))
            {
                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

                var userId = await command.ExecuteScalarAsync();
                user.Id = Convert.ToInt32(userId);

                return user.Id;
            }
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("DELETE FROM Users WHERE UserId = @id", connection))
            {
                command.Parameters.AddWithValue("@id", id);
                return await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = new List<User>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT * FROM Users", connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User
                        {
                            Id = (int) reader.GetDecimal(reader.GetOrdinal("UserId")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            PhoneNumber = (long)reader.GetDecimal(reader.GetOrdinal("PhoneNumber"))
                        });
                    }
                }
            }
        }
        return users;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("SELECT * FROM Users WHERE UserId = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new User
                        {
                            Id = (int)reader.GetDecimal(reader.GetOrdinal("UserId")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            PhoneNumber = (long)reader.GetDecimal(reader.GetOrdinal("PhoneNumber"))
                        };
                    }
                }
            }
        }
        return null;
    }

    public async Task UpdateAsync(User user)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand("UPDATE Users SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, PhoneNumber = @PhoneNumber, Email = @Email WHERE UserId = @Id", connection))
            {
                command.Parameters.AddWithValue("@FirstName", user.Id);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                command.Parameters.AddWithValue("@Email", user.Email);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
