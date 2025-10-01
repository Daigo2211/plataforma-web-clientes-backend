using Microsoft.EntityFrameworkCore;
using Maqui.Domain.Entities;
using Maqui.Infrastructure.Configurations;

namespace Maqui.Infrastructure.Data.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
    }
}