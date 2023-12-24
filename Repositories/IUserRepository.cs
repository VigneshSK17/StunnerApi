public interface IUserRepository {
    Task<User?> Authenticate(string username, string password);
    Task<List<string>> GetUsernames();
    Task<User?> GetUserByID(int userId);

    Task<User?> CreateUser(string username, string password);
    Task<bool> DeleteUser(int userId);
    // Task<bool> UpdateUser(int userId, string newUsername, string newPassword);

    // Activities
    Task<List<ActivityJson>> GetActivities(int userId);
    Task<int?> CreateActivity(
        int userId,
        string title,
        string dateCreated,
        ActivityType activityType = ActivityType.OTHER,
        string? subject = null);
    Task<bool> DeleteActivity(int userId);
    Task<bool> UpdateActivity(
        int activityId,
        string title,
        string dateUpdated,
        ActivityType activityType = ActivityType.OTHER,
        string? subject = null
    );
}