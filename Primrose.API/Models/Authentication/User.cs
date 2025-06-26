using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Primrose.API.Models.Authentication;

[Table("users")]
public sealed class User
{
    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; } = Guid.NewGuid();

    [Column("email")]
    public required string Email { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("password_hash")]
    public required string PasswordHash { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("email_is_verified")]
    public bool EmailIsVerified { get; set; } = false;

    [Column("created_at")]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    [Column("last_login")]
    public DateTimeOffset? LastLogin { get; set; } = null;

    [Column("password_last_changed_at")]
    public DateTimeOffset? PasswordLastChangedAt { get; set; } = null;
}