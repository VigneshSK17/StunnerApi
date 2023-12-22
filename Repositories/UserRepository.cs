public class UserRepository(DatabaseContext _context) : IUserRepository {

    private List<User> _users = [
        new User
        {Username="peter", Password="peter123"},
        new User
        {Username="joydip", Password="joydip123"},
        new User
        {Username="james", Password="james123"}
    ];

    public async Task<User?> Authenticate(string username, string password) {
        List<User> users = _context.Users.Where(u => u.Username == username).ToList();
        User? user = users.SingleOrDefault(u => BC.EnhancedVerify(password, u.Password));

        return await Task.FromResult(user);
    }

    // TODO: Add some sort of exception return for if user already exists
    public async Task<User?> CreateUser(string username, string password) {
        if (await Authenticate(username, password) == null) {
            _context.Users.Add(
                new User {
                    Username = username,
                    Password = BC.EnhancedHashPassword(password, 17)
                }
            );
            await _context.SaveChangesAsync();
            return await Task.FromResult(_users.Last());
        } else {
            return await Task.FromResult<User?>(null);
        }
    }

    public async Task<User?> GetUserByID(int userId) {
        User? user = _context.Users.FirstOrDefault(u => u.Id == userId);
        return await Task.FromResult(user);
    }

    public async Task<List<string>> GetUsernames() {
        List<string> usernames = _context.Users.Select(user => user.Username).ToList();
        return await Task.FromResult(usernames);
    }

    public async Task<bool> DeleteUser(int userId) {
        User? user = await GetUserByID(userId);
        if (user != null) {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        } else {
            return false;
        }
    }

    public async Task<List<ActivityJson>> GetActivities(int userId) {
        List<ActivityJson> activities = [.. _context.Activities
            .Where(a => userId == a.UserId)
            .Select(a => new ActivityJson(a))];
        return await Task.FromResult(activities);
    }

    public async Task<int?> CreateActivity(
        int userId,
        string title,
        string dateCreated,
        ActivityType activityType = ActivityType.OTHER,
        string? subject = null
        ) {

        User? user = _context.Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) {
            return null;
        }

        if (!DateTimeOffset.TryParse(dateCreated, out DateTimeOffset offset))
        {
            offset = DateTimeOffset.UtcNow;
        }


        _context.Activities.Add(new Activity{
            Title = title,
            ActivityType = activityType,
            Subject = subject,
            DateCreated = offset,
            User = user
        });
        await _context.SaveChangesAsync();

        Activity newActivity = _context.Activities.First(a => offset == a.DateCreated);
        user.Activities.Add(newActivity);
        await _context.SaveChangesAsync();


        return await Task.FromResult<int?>(newActivity.Id);
    }



}