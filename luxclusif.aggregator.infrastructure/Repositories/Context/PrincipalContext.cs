using Microsoft.EntityFrameworkCore;
using DomainEntity = luxclusif.aggregator.domain.Entity;

namespace luxclusif.aggregator.infrastructure.Repositories.Context;
public class PrincipalContext : DbContext
{
    public PrincipalContext(DbContextOptions<PrincipalContext> options) : base(options)
    {

    }

    public DbSet<DomainEntity.TotalExpendedInOrdersAggregated> TotalExpendedInOrdersAggregated => Set<DomainEntity.TotalExpendedInOrdersAggregated>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<DomainEntity.TotalExpendedInOrdersAggregated>((v) => {
                v.HasKey(k => k.Id);
                v.Property(k => k.UserId);
                v.Property(k => k.Value);
                v.Property(k => k.CreatedAt);
                v.Property(k => k.LastUpdateAt);
            });
    }
}
