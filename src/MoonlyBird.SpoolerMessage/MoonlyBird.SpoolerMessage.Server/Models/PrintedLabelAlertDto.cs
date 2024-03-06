using MassTransit;

namespace MoonlyBird.SpoolerMessage.Server.Models
{    
    public record PrintedLabelAlertDto
    {
        public string MessageId { get; set; } = string.Empty;

        public bool IsPrinted { get; set; } = false;
    }
}
