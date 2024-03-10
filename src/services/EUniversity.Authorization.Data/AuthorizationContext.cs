using EUniversity.Authorization.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EUniversity.Authorization.Data;

public class AuthorizationContext : DbContext
{
    public AuthorizationContext(DbContextOptions<AuthorizationContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
}
