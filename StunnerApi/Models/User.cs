public class User(string _username, string _password)
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; } = _username;
    public string Password { get; set; } = _password;
}