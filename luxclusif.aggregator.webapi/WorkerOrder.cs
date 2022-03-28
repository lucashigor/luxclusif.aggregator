using luxclusif.aggregator.application.Constants;
using luxclusif.aggregator.application.Interfaces;
using luxclusif.aggregator.application.UseCases.Order.CreateTotalExpendedInOrdersAggregated;
using luxclusif.aggregator.infrastructure.rabbitmq;
using MediatR;

namespace luxclusif.aggregator.webapi
{
    public class WorkerOrder : BackgroundService
    {
        private readonly IMessageReceiverInterface busControl;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public WorkerOrder(IServiceScopeFactory serviceScopeFactory)
        {
            this.busControl = RabbitHutch.CreateBus("luxclusif-aggregator-rabbitmq");
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await busControl.ReceiveAsync<AddValueToTotalOfUserInput>(Queue.Order, x =>
            {
                Task.Run(async () => await DidJob(x, stoppingToken));
            });
        }

        private async Task DidJob(AddValueToTotalOfUserInput item, CancellationToken cancellationToken)
        {
            using var scope = serviceScopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(item, cancellationToken);
        }
    }
}
