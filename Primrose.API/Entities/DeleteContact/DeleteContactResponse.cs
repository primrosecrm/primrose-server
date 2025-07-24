namespace Primrose.Entities.DeleteContact;

public sealed class DeleteContactResponse
    : ApiResponse
{
    public bool IsDeleted { get; set; }
}