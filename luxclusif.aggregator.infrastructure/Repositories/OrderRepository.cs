using luxclusif.aggregator.domain.Entity;
using luxclusif.aggregator.domain.Repository;
using luxclusif.aggregator.domain.SeedWork.ShearchableRepository;
using luxclusif.aggregator.infrastructure.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using DomainEntity = luxclusif.aggregator.domain.Entity;

namespace luxclusif.aggregator.infrastructure.Repositories;
public class OrderRepository
    : IOrderRepository
{

    private readonly DbSet<TotalExpendedInOrdersAggregated> dbset;

    public OrderRepository(PrincipalContext context)
    {
        dbset = context.TotalExpendedInOrdersAggregated;
    }


    public async Task Insert(DomainEntity.TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken)
    => await dbset.AddAsync(user, cancellationToken);

    public async Task<DomainEntity.TotalExpendedInOrdersAggregated> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var item = await dbset.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        return item!;
    }

    public Task Update(DomainEntity.TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken)
    {
        dbset.Attach(user);

        return Task.CompletedTask;
    }

    public async Task<SearchOutput<DomainEntity.TotalExpendedInOrdersAggregated>> Search(SearchInput input, CancellationToken cancellationToken)
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

    private static IQueryable<DomainEntity.TotalExpendedInOrdersAggregated> AddOrderToquery(
        IQueryable<DomainEntity.TotalExpendedInOrdersAggregated> query,
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
