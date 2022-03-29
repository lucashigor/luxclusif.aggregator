using luxclusif.aggregator.domain.Entity;
using luxclusif.aggregator.domain.Repository;
using luxclusif.aggregator.domain.SeedWork.ShearchableRepository;
using luxclusif.aggregator.infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace luxclusif.aggregator.infrastructure.Repositories;
public class OrderRepository
    : IOrderRepository
{
    private readonly IConfiguration _configuration;

    private readonly DbSet<TotalExpendedInOrdersAggregated> dbset;

    public OrderRepository(PrincipalContext context, IConfiguration configuration)
    {
        dbset = context.TotalExpendedInOrdersAggregated;
        _configuration = configuration;
    }

    public async Task InsertSynchronous(TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken)
    {
        DbContextOptions<PrincipalContext> contextOptions = getDbContextOptions();

        using var context = new PrincipalContext(contextOptions);
        await context.TotalExpendedInOrdersAggregated.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    private DbContextOptions<PrincipalContext> getDbContextOptions()
    {
        var conn = _configuration.GetConnectionString("PrincipalDatabase");

        var contextOptions = new DbContextOptionsBuilder<PrincipalContext>()
            .UseNpgsql(conn)
            .Options;
        return contextOptions;
    }

    public async Task<TotalExpendedInOrdersAggregated> GetByUserIdSynchronous(Guid userId, CancellationToken cancellationToken)
    {
        DbContextOptions<PrincipalContext> contextOptions = getDbContextOptions();

        using var context = new PrincipalContext(contextOptions);
        var item = await context.TotalExpendedInOrdersAggregated
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        return item!;
    }

    public async Task UpdateSynchronous(TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken)
    {
        DbContextOptions<PrincipalContext> contextOptions = getDbContextOptions();

        using var context = new PrincipalContext(contextOptions);

        context.TotalExpendedInOrdersAggregated.Attach(user);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Insert(TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken)
    => await dbset.AddAsync(user, cancellationToken);

    public async Task<TotalExpendedInOrdersAggregated> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var item = await dbset.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        return item!;
    }

    public Task Update(TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken)
    {
        dbset.Attach(user);

        return Task.CompletedTask;
    }

    public async Task<SearchOutput<TotalExpendedInOrdersAggregated>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = dbset.AsNoTracking();

        query = AddOrderToquery(query, input.OrderBy, input.Order);
        if (!string.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Name.Contains(input.Search));

        var total = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync(cancellationToken);

        return new(input.Page, input.PerPage, total, items);
    }

    private static IQueryable<TotalExpendedInOrdersAggregated> AddOrderToquery(
        IQueryable<TotalExpendedInOrdersAggregated> query,
        string orderProperty,
        SearchOrder order)
    => (orderProperty.ToLower(), order) switch
    {
        ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name),
        ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name),
        ("value", SearchOrder.Asc) => query.OrderBy(x => x.Value),
        ("value", SearchOrder.Desc) => query.OrderByDescending(x => x.Value),
        _ => query.OrderBy(x => x.Name)
    };
}
