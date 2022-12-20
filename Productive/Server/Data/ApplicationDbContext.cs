using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Productive.Server.Models;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Productive.Server.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public string CurrentUserId { get; set; } = "Anonymous";

    public DbSet<Models.Client> Clients { get; set; }

    public DbSet<ClientCellPhone> ClientCellPhones { get; set; }
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder
        .Entity<Models.Client>()
        .HasIndex(u => u.Email)
        .IsUnique();
        builder
        .Entity<Models.ClientCellPhone>()
        .HasIndex(u => u.CellPhone)
        .IsUnique();

    }
    public override int SaveChanges()
    {
        UpdateAuditEntities();
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateAuditEntities();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        UpdateAuditEntities();
        return base.SaveChangesAsync(cancellationToken);
    }


    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        UpdateAuditEntities();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    private void UpdateAuditEntities()
    {
        var modifiedEntries = ChangeTracker.Entries()
            .Where(x => x.Entity is AuditableEntity &&
                        (x.State == EntityState.Added || x.State == EntityState.Modified));


        foreach (var entry in modifiedEntries)
        {
            var entity = (AuditableEntity)entry.Entity;
            DateTime now = DateTime.UtcNow;
            Thread.Sleep(new TimeSpan(500));
            if (entry.State == EntityState.Added)
            {
                entity.CreatedDate = now;
                entity.CreatedBy = CurrentUserId;
            }
            else
            {
                base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
            }

            entity.UpdatedDate = now;
            entity.UpdatedBy = CurrentUserId;
        }
    }
   
}

