using luxclusif.aggregator.domain.Entity;
using luxclusif.aggregator.domain.SeedWork;
using luxclusif.aggregator.domain.SeedWork.ShearchableRepository;

namespace luxclusif.aggregator.domain.Repository;
public interface IOrderRepository : IRepository, IShearchableRepository<TotalExpendedInOrdersAggregated>
{
    Task Insert(TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken);
    Task<TotalExpendedInOrdersAggregated> GetByUserId(Guid userId, CancellationToken cancellationToken);
    Task Update(TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken);
}
