using Sample.Observability.Infrastructure.Extensions;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.AddSerilog(builder.Configuration, "API Observability");
    Log.Information("Starting API");

    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddElasticsearch(builder.Configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilog();

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}