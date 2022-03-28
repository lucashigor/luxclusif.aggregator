namespace luxclusif.aggregator.application.Interfaces;
public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);
    public Task RollbackAsync(CancellationToken cancellationToken);
}
