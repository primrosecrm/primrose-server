using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Primrose.Models.Authentication;

// this is an example template to aid the creation of a new data model

#pragma warning disable CS8618

[Table("some_table")]
public sealed class SomeClass : BaseModel
{
    [PrimaryKey("some_id", false)]
    public Guid SomeId { get; set; } = Guid.NewGuid();


}

#pragma warning restore CS8618