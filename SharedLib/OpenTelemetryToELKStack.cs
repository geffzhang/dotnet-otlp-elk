using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Exporter;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.Extensions.Options;
using OpenTelemetry;

namespace SharedLib
{
    public static class OpenTelemetryToELKStack
    {
        public static void AddOpenTelemetryToELKStack(this WebApplicationBuilder builder, string servicename, string endpointurl)
        {

            //OTLP Test
            //https://bartwullems.blogspot.com/2021/12/elastic-apmuse-net-opentelemetry.html

            //https://dev.to/jmourtada/how-to-setup-opentelemetry-instrumentation-in-aspnet-core-23p5
            //dotnet add package --prerelease OpenTelemetry.Exporter.Console
            //dotnet add package --prerelease OpenTelemetry.Exporter.OpenTelemetryProtocol
            //dotnet add package --prerelease OpenTelemetry.Exporter.OpenTelemetryProtocol.Logs
            //dotnet add package --prerelease OpenTelemetry.Extensions.Hosting
            //dotnet add package --prerelease OpenTelemetry.Instrumentation.AspNetCore
            //dotnet add package --prerelease OpenTelemetry.Instrumentation.Http
            //dotnet add package --prerelease OpenTelemetry.Instrumentation.Runtime

            //string servicename = "my-service-name";
            //string endpointurl = "http://localhost:8200";



            builder.Services.AddOpenTelemetry()
                .ConfigureResource(builder => builder.AddService(serviceName: servicename))
                .WithTracing(builder => builder
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter(configure =>
                    {
                        configure.Endpoint = new Uri("http://otel-collector:4318/v1/traces"); // OTel HTTP port + tracing endpoint
                        configure.Protocol = OtlpExportProtocol.HttpProtobuf; // Default is gRPC - switching to HTTP
                        configure.ExportProcessorType = ExportProcessorType.Simple;
                    })
                )
                .WithMetrics(builder => builder
                    // Configure the resource attribute `service.name` to MyServiceName
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(servicename))
                    // Add metrics from the AspNetCore instrumentation library
                    .AddRuntimeInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()
                    .AddOtlpExporter(configure =>
                    {
                        configure.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                        configure.Endpoint = new Uri("http://otel-collector:4318/v1/metrics");
                    })
                );           
        }
   }
}
