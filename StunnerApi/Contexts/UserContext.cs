using Microsoft.EntityFrameworkCore;

public class UserContext(DbContextOptions options) : DbContext(options) {
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id);
            entity.Property(e => e.Username).HasMaxLength(250);
            entity.Property(e => e.Password).HasMaxLength(250);

            entity.HasData(new User {
                Id = 1,
                Username = "vigsk17",
                Password = "abc123",
            });
        });
    }
}