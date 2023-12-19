public enum ActivityType { HW, PROJECT, QUIZ, EXAM, OTHER }

public class Activity {
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Title { get; set; }
    public ActivityType ActivityType { get; set; } = ActivityType.OTHER;
    public string? Subject { get; set; }

    public DateTimeOffset DateCreated { get; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DateUpdated { get; set; }

    public User User { get; set; } = null!;
}