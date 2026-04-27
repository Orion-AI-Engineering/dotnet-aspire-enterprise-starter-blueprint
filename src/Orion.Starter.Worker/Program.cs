using Orion.Starter.ServiceDefaults;
using Orion.Starter.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddHostedService<OrderProcessorWorker>();

var host = builder.Build();
host.Run();
