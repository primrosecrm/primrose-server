using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Primrose.API.Models;

// This class represents the result of a HTTP API operation.
// Success is set on whether an unexpected error has occurred.
public abstract class IResponse
{
    public bool Success { get; set; } = true;
    public string? Error { get; set; } = null;
}