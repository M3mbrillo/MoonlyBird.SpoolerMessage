﻿using MassTransit;

namespace MoonlyBird.SpoolerMessage.Server.Models
{
    [EntityName("print-label")]
    public record PrintLabelDto
    {
        
        public string Label { get; set; } = string.Empty;

        public string MessageId { get; set; } = string.Empty;
    }
}
