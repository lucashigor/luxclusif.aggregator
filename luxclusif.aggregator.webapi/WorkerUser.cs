using luxclusif.aggregator.application.Constants;
using luxclusif.aggregator.application.Interfaces;
using luxclusif.aggregator.application.Models;
using luxclusif.aggregator.application.UseCases.Order.CreateTotalExpendedInOrdersAggregated;
using luxclusif.aggregator.domain.Repository;
using luxclusif.aggregator.infrastructure.rabbitmq;
using MediatR;
using Newtonsoft.Json;

namespace luxclusif.aggregator.webapi
{
    public class WorkerUser : BackgroundService
    {
        private readonly IMessageReceiverInterface busControl;
        private readonly IServiceScopeFactory serviceScopeFactory;
        public WorkerUser(IServiceScopeFactory serviceScopeFactory)
        {
            busControl = RabbitHutch.CreateBus("luxclusif-aggregator-rabbitmq");
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await busControl.ReceiveAsync<CreateUserInput>(Queue.User, x =>
            {
                Task.Run(async () => await DidJob(x, stoppingToken));
            });
        }

        private async Task DidJob(CreateUserInput item, CancellationToken cancellationToken)
        {
            using var scope = serviceScopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Send(item, cancellationToken);
        }
    }
}
