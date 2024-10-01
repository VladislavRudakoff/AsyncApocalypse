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

app.MapGet("/async-work", async () =>
{
    // Имитируем долгую работу (например, 3 секунды) с использованием Task.Delay
    await Task.Delay(3000);

    return Results.Ok("Асинхронная работа завершена");
});

await app.RunAsync();
