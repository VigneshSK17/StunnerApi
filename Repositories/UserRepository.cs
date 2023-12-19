using Microsoft.Extensions.Logging.Abstractions;

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
}