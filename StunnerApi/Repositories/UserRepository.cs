
public class UserRepository : IUserRepository {

    private readonly List<User> _users = [
        new User
        {
            Id = 1, Username = "peter", Password = "peter123"
        },
        new User
        {
            Id = 2, Username = "joydip", Password = "joydip123"
        },
        new User
        {
            Id = 3, Username = "james", Password = "james123"
        }
    ];

    public async Task<bool> Authenticate(string username, string password) {
        // TODO: Change this to include password hashing
        return await Task.FromResult(_users.SingleOrDefault(x => x.Username == username && x.Password == password) != null);
    }

    public async Task<List<string>> GetUsernames() {
        List<string> usernames = _users.Select(user => user.Username).ToList();
        return await Task.FromResult(usernames);
    }
}