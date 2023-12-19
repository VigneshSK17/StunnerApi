using Microsoft.EntityFrameworkCore;

public class DatabaseContext(DbContextOptions options) : DbContext(options) {
    public DbSet<User> Users { get; set; }
    public DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<User>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<User>()
            .HasData(new User {
                Id = 1,
                Username = "vigsk17",
                Password = BC.EnhancedHashPassword("abc123", 17),
            });

        modelBuilder.Entity<Activity>()
            .HasKey(e => e.Id);
        modelBuilder.Entity<Activity>()
            .HasOne(e => e.User)
            .WithMany(e => e.Activities)
            .HasForeignKey(e => e.UserId);

    }

}