using Apache.NMS.ActiveMQ.Commands;
using MassTransit;
using MoonlyBird.SpoolerMessage.Server.Consumers;
using System.Runtime.CompilerServices;

namespace MoonlyBird.SpoolerMessage.Server.ConfigureService
{
    public static class ConfigureMassTransitExtensions
    {
        public static void ConfigureMassTransitServices(this IServiceCollection services)
        {
            services.AddMassTransit(busRegistration =>
            {

                busRegistration.AddConsumer<PrintLabelConsumer, PrintLabelConsumerDefinition>();

                busRegistration.UsingActiveMq((busContext, factoryConfiguration) =>
                {

                    factoryConfiguration.Host("activemq://localhost:61616/", configureHost =>
                    {
                        configureHost.Username("artemis");
                        configureHost.Password("artemis");
                    });

                    factoryConfiguration.ConfigureEndpoints(busContext);

                });

            });
        }
    }
}
