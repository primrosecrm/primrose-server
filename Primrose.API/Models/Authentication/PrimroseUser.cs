using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Primrose.Models.Authentication;

#pragma warning disable CS8618

[Table("users")]
public sealed class PrimroseUser : BaseModel
{
    [PrimaryKey("user_id", false)]
    public Guid UserId { get; set; } = Guid.NewGuid();

    [Column("email")]
    public string Email { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("password_hash")]
    public string PasswordHash { get; set; }

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

#pragma warning restore CS8618