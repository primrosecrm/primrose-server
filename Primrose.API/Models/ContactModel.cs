using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Primrose.Models;

#pragma warning disable CS8618

[Table("contacts")]
public sealed class ContactModel : BaseModel
{
    [PrimaryKey("contact_id", false)]
    public Guid ContactId { get; set; } = Guid.NewGuid();

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("name")]
    public string Name { get; set; }
}

#pragma warning restore CS8618