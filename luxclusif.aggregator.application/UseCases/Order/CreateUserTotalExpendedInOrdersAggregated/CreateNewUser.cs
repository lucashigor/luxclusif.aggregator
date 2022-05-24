using Hangfire;
using luxclusif.aggregator.domain.Entity;
using luxclusif.aggregator.domain.Repository;
using MediatR;

namespace luxclusif.aggregator.application.UseCases.Order.CreateUserTotalExpendedInOrdersAggregated
{
    public class CreateNewUser :
    IRequestHandler<CreateUserInput>
    {
        public IOrderRepository repository;

        public CreateNewUser(IOrderRepository repository)
        {
            this.repository = repository;
        }

        public Task<Unit> Handle(CreateUserInput request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => HandleEnqueued(request));

            return Task.FromResult(Unit.Value);
        }

        public async Task HandleEnqueued(CreateUserInput request)
        {
            var item = new TotalExpendedInOrdersAggregated(request.Name, request.Id, 0);

            var alreadDatabase = await repository.GetByUserIdSynchronous(item.UserId, CancellationToken.None);

            if(alreadDatabase != null)
            {
                return;
            }

            await repository.InsertSynchronous(item, CancellationToken.None);
        }
    }
}
