using Serilog;

namespace MoonlyBird.SpoolerMessage.Server.ConfigureService
{
    public static class ConfigureSerilogExtension
    {
        public static void ImplementSerilog(this WebApplicationBuilder host)
        {
            host.Host.UseSerilog();
        }


        public static void UseSerilog(this WebApplication app)
        {
            app.UseSerilogRequestLogging();
        }

    }
}
