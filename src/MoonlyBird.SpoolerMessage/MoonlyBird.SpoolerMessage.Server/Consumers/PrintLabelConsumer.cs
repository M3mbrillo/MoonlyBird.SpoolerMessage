using MassTransit;
using MassTransit.DependencyInjection;
using MassTransit.KafkaIntegration;
using MoonlyBird.SpoolerMessage.Server.ConfigureService;
using MoonlyBird.SpoolerMessage.Server.Models;

namespace MoonlyBird.SpoolerMessage.Server.Consumers
{
    public class PrintLabelConsumer(
        ILogger<PrintLabelConsumer> logger,
        ITopicProducer<PrintedLabelAlertDto> producer
        ) : MassTransit.IConsumer<PrintLabelDto>
    {
        private readonly ITopicProducer<PrintedLabelAlertDto> Producer = producer;
        private ILogger<PrintLabelConsumer> Logger { get; } = logger;

        public async Task Consume(ConsumeContext<PrintLabelDto> context)
        {
            Logger.LogInformation("Label: {?}", context.Message.Label);
            await Task.Delay(1000, context.CancellationToken);

            await Producer.Produce(new
            {
                MessageId = 1,
                IsPrinted = true
            });
        }
    }


    public class PrintLabelConsumerDefinition :
        ConsumerDefinition<PrintLabelConsumer>
    {
        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<PrintLabelConsumer> consumerConfigurator,
            IRegistrationContext registrationContext
        )
        {           
            endpointConfigurator.ConfigureConsumeTopology = false;
            endpointConfigurator.ClearSerialization();

            endpointConfigurator.UseRawJsonSerializer();
        }
    }
}
