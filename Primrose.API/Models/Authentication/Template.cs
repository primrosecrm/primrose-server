using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Primrose.Models.Authentication;

#pragma warning disable CS8618

[Table("some_table")]
public sealed class SomeClass : BaseModel
{
    [PrimaryKey("some_id", false)]
    public Guid SomeId { get; set; } = Guid.NewGuid();


}

#pragma warning restore CS8618