using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User()
{
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}