public class User() {
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}