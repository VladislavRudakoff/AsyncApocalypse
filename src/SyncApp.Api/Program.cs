WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

OpenTelemetryBuilder otel = builder.Services.AddOpenTelemetry();

otel.WithMetrics(metrics => metrics
    .AddPrometheusExporter()
    .AddRuntimeInstrumentation()
    .AddProcessInstrumentation()
    .AddAspNetCoreInstrumentation());

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

WebApplication app = builder.Build();

app.UseSerilogRequestLogging();

app.MapPrometheusScrapingEndpoint();

app.MapGet("/sync-work", () =>
{
    // Имитируем долгую работу (например, 3 секунды)
    Thread.Sleep(3000);

    return Results.Ok("Синхронная работа завершена");
});

app.Run();
