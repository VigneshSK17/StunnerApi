
public class UserRepository : IUserRepository {

    private List<User> _users = [
        new User
        ("peter", BC.EnhancedHashPassword("peter123", 17)),
        new User
        ("joydip", "joydip123"),
        new User
        ("james", "james123")
    ];

    public async Task<bool> Authenticate(string username, string password) {
        return await Task.FromResult(_users.SingleOrDefault(u => u.Username == username && BC.EnhancedVerify(password, u.Password)) != null);
    }

    // TODO: Add some sort of exception return for if user already exists
    public async Task<bool> Create(string username, string password) {
        if (!await Authenticate(username, password)) {
            _users.Add(
                new User(
                    username,
                    BC.EnhancedHashPassword(password, 17)
                )
            );
            return await Task.FromResult(true);
        } else {
            return await Task.FromResult(false);
        }
    }

    public async Task<List<string>> GetUsernames() {
        List<string> usernames = _users.Select(user => user.Username).ToList();
        return await Task.FromResult(usernames);
    }
}