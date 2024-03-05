using MassTransit;
using MoonlyBird.SpoolerMessage.Server.Models;

namespace MoonlyBird.SpoolerMessage.Server.Consumers
{
    public class PrintLabelConsumer : MassTransit.IConsumer<PrintLabelDto>
    {

        public PrintLabelConsumer(ILogger<PrintLabelConsumer> logger)
        {
            Logger = logger;
        }

        public ILogger<PrintLabelConsumer> Logger { get; }

        public async Task Consume(ConsumeContext<PrintLabelDto> context)
        {
            Logger.LogInformation(context.Message.Label);
        }
    }


    public class PrintLabelConsumerDefinition :
        ConsumerDefinition<PrintLabelConsumer>
    {
        protected override void ConfigureConsumer(
            IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<PrintLabelConsumer> consumerConfigurator
        )
        {
            endpointConfigurator.ConfigureConsumeTopology = false;
            endpointConfigurator.ClearSerialization();

            endpointConfigurator.UseRawJsonSerializer();
        }
    }
}
