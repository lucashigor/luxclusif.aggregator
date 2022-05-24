using luxclusif.aggregator.domain.Entity;
using luxclusif.aggregator.domain.SeedWork;
using luxclusif.aggregator.domain.SeedWork.ShearchableRepository;

namespace luxclusif.aggregator.domain.Repository;
public interface IOrderRepository : IRepository, IShearchableRepository<TotalExpendedInOrdersAggregated>
{
    Task Insert(TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken);
    Task<TotalExpendedInOrdersAggregated> GetByUserId(Guid userId, CancellationToken cancellationToken);
    Task Update(TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken);
    Task InsertSynchronous(TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken);
    Task UpdateSynchronous(TotalExpendedInOrdersAggregated user, CancellationToken cancellationToken);
    Task<TotalExpendedInOrdersAggregated> GetByUserIdSynchronous(Guid userId, CancellationToken cancellationToken);
}
