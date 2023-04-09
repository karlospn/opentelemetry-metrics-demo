using BookStore.Infrastructure.Metrics;
using BookStore.WebApi.Middleware;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "BookStore API",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.RegisterInfrastureDependencies(builder.Configuration);

var meters = new OtelMetrics();

builder.Services.AddOpenTelemetry().WithMetrics(opts => opts
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("BookStore.WebApi"))
    .AddMeter(meters.MetricName)
    .AddAspNetCoreInstrumentation()
    .AddProcessInstrumentation()
    .AddRuntimeInstrumentation()
    .AddView(
        instrumentName: "orders-price",
        new ExplicitBucketHistogramConfiguration { Boundaries = new double[] { 15, 30, 45, 60, 75 } })
    .AddView(
        instrumentName: "orders-number-of-books",
        new ExplicitBucketHistogramConfiguration { Boundaries = new double[] { 1, 2, 5 } })
    .AddOtlpExporter(options  =>
    {
        options.Endpoint = new Uri(builder.Configuration["Otlp:Endpoint"] 
                                   ?? throw new InvalidOperationException());
    }));

var app = builder.Build();

// Add simulated latency to improve http requests avg. time dashboard
app.UseSimulatedLatency(
    min: TimeSpan.FromMilliseconds(500),
    max: TimeSpan.FromMilliseconds(1000)
);
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
