using MassTransit;
using MassTransit.KafkaIntegration;
using MoonlyBird.SpoolerMessage.Server.Consumers;
using MoonlyBird.SpoolerMessage.Server.Models;

namespace MoonlyBird.SpoolerMessage.Server.ConfigureService
{
    public static class ConfigureMassTransitExtensions
    {
        public static void ConfigureMassTransitServices(this IServiceCollection services)
        {
            services.AddMassTransit<IActiveMqBus>(busRegistration =>
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


            services.AddMassTransit<IKafkaBus>(busRegistration =>
            {
                busRegistration.UsingInMemory();

                busRegistration.AddRider((rider) =>
                {
                    rider.AddProducer<PrintedLabelAlertDto>("printed-label-alert");

                    rider.UsingKafka((registrationContext, kafkaFactoryConfigurator) =>
                    {
                        kafkaFactoryConfigurator.Host("localhost:9094");
                    });
                });
            });
        }
    }
    
    public interface IKafkaBus : IBus { }

    public interface IActiveMqBus : IBus { }
}
