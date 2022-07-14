using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Sample.Observability.Infrastructure.ViewModels;

namespace Sample.Observability.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LogController : ControllerBase
{
    private readonly IElasticClient _elasticClient;

    public LogController(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllLogs()
    {
        var search = new SearchDescriptor<LogViewModel>("indexlogs")
            .MatchAll()
            .Size(100)
            .Sort(descriptor => descriptor.Descending(p => p.Timestamp));

        var response = await _elasticClient.SearchAsync<LogViewModel>(search);

        if (!response.IsValid)
            throw new ElasticsearchClientException(response?.ServerError?.ToString());

        return Ok(response.Documents.ToList());
    }

    [HttpGet("date")]
    public async Task<IActionResult> GetLogsByDate([FromQuery] DateTime dateBegin, [FromQuery] DateTime dateEnd)
    {
        var search = new QueryContainerDescriptor<LogViewModel>()
            .Bool(b => b.Filter(f => f.DateRange(dt => dt
                .Field(field => field.Timestamp)
                .GreaterThanOrEquals(dateBegin)
                .LessThanOrEquals(dateEnd)
                .TimeZone("+00:00"))));

        var response = await _elasticClient.SearchAsync<LogViewModel>(descriptor => descriptor
            .Index("indexlogs")
            .Size(100)
            .Sort(sort => sort.Descending(p => p.Timestamp))
            .Query(_ => search));

        if (!response.IsValid)
            throw new ElasticsearchClientException(response?.ServerError?.ToString());

        return Ok(response.Documents.ToList());
    }
}