namespace Sample.Observability.Infrastructure.ViewModels;
public class LogViewModel
{
    [Nest.PropertyName("@timestamp")]
    public DateTimeOffset Timestamp { get; set;  }
    [Nest.PropertyName("message")]
    public string? Message { get; set; }
    [Nest.PropertyName("log.level")]
    public string? Level { get; set; }
    [Nest.PropertyName("metadata")]
    public MetadataViewModel? Metadata { get; set; }
    [Nest.PropertyName("process")]
    public ProcessViewModel? Process { get; set; }
}

public class MetadataViewModel
{
    [Nest.PropertyName("message_template")]
    public string? MessageTemplate { get; set; }
    [Nest.PropertyName("correlation_id")]
    public string? CorrelationId { get; set; }
}

public class ProcessViewModel
{
    [Nest.PropertyName("name")]
    public string? ApplicationName { get; set; }
}