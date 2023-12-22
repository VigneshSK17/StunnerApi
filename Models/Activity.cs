public enum ActivityType { HW, PROJECT, QUIZ, EXAM, OTHER }

public class Activity {
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Title { get; set; }
    public ActivityType ActivityType { get; set; } = ActivityType.OTHER;
    public string? Subject { get; set; }

    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }

    public User User { get; set; } = null!;
}

public class ActivityJson(Activity activity)
{
    public int Id { get; set; } = activity.Id;
    public int UserId { get; set; } = activity.UserId;
    public string Title { get; set; } = activity.Title;
    public ActivityType ActivityType { get; set; } = activity.ActivityType;
    public string? Subject { get; set; } = activity.Subject;

    public DateTimeOffset DateCreated { get; set; } = activity.DateCreated;
    public DateTimeOffset? DateUpdated { get; set; } = activity.DateUpdated;
}