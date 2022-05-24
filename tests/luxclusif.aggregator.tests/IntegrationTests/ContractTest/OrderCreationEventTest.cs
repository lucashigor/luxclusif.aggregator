using luxclusif.aggregator.application.UseCases.Order.AddTotalExpendedInOrdersAggregated;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Matchers;
using Xunit;
using Xunit.Abstractions;
using System;
using System.Threading.Tasks;

namespace luxclusif.aggregator.tests.IntegrationTests.ContractTest
{
    public class StockEventProcessorTests
    {
        private readonly IMessagePactBuilderV3 messagePact;

        public string microServiceIntegrationName { get; set; } = "luxclusif.order.webapi";
        public string microServiceExchangeName { get; set; } = "topic.createdorder";

        public StockEventProcessorTests()
        {
            IMessagePactV3 v3 = MessagePact.V3("Aggregator Event Consumer", microServiceIntegrationName, new PactConfig
            {
                // the location in which the pact file is written
                PactDir = "../../../pacts/",

                // the settings used to serialise each message by default
                DefaultJsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            });

            this.messagePact = v3.UsingNativeBackend();
        }

        [Fact]
        public void RecieveSomeStockEvents()
        {
            var userId = Guid.NewGuid();

            // define your expected message, using matchers for the message body
            this.messagePact
                .ExpectsToReceive(microServiceExchangeName)
                .WithJsonContent(Match.MinType(new
                {
                    UserId = Match.Type(userId),
                    Value = Match.Type(1.23m)
                }, 1))
                .Verify<ICollection<AddValueToTotalOfUserInput>>(events =>
                {
                // here we simply make sure it's expected, but we could run it through a real event processor
                // to make sure it was processed properly. Warning - this is not meant for integration testing!
                events.Should().BeEquivalentTo(new[]
                    {
                    new AddValueToTotalOfUserInput
                    {
                        UserId = userId,
                        Value = 1.23m
                    }
                    });
                });
        }
    }
}