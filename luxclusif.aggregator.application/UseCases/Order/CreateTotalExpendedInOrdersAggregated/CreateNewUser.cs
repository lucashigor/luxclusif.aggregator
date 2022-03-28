using luxclusif.aggregator.application.Interfaces;
using luxclusif.aggregator.domain.Entity;
using luxclusif.aggregator.domain.Repository;
using MediatR;

namespace luxclusif.aggregator.application.UseCases.Order.CreateTotalExpendedInOrdersAggregated
{
    public class CreateNewUser :
    IRequestHandler<CreateUserInput>
    {
        public IOrderRepository repository;
        public IUnitOfWork unitOfWork;

        public CreateNewUser(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateUserInput request, CancellationToken cancellationToken)
        {
            var item = new TotalExpendedInOrdersAggregated(request.Name,request.UserId,0);

            await repository.Insert(item, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
