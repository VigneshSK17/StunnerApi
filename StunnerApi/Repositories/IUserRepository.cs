public interface IUserRepository {
    Task<User?> Authenticate(string username, string password);
    Task<List<string>> GetUsernames();
    Task<User?> GetUserByID(int userId);

    Task<User?> CreateUser(string username, string password);
    Task<bool> DeleteUser(int userId);
    // Task<bool> UpdateUser(int userId, string newUsername, string newPassword);
}