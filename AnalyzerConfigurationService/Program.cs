using AnalyzerConfigurationService;
using AnalyzerConfigurationService.Services;
using Microsoft.Extensions.Hosting.WindowsServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService();
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IAnalyzerLoggerFactory, AnalyzerLoggerFactory>();
builder.Services.AddSingleton<AnalyzerManager>();

var host = builder.Build();
host.Run();
