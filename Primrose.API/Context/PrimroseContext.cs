using Microsoft.EntityFrameworkCore;
using Primrose.API.Models.Authentication;

namespace Primrose.API.Context;

public class PrimroseContext : DbContext
{
    public PrimroseContext(DbContextOptions<PrimroseContext> options)
        : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
}