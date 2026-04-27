var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgresdb").WithPgAdmin();
var appDb = postgres.AddDatabase("appDb");

var redis = builder.AddRedis("redis");
var rabbit = builder.AddRabbitMQ("rabbitmq");

var api = builder.AddProject<Projects.Orion_Starter_Api>("api")
    .WithReference(appDb).WaitFor(appDb)
    .WithReference(redis).WaitFor(redis)
    .WithReference(rabbit).WaitFor(rabbit);

builder.AddProject<Projects.Orion_Starter_Web>("web")
    .WithReference(api).WaitFor(api);

builder.AddProject<Projects.Orion_Starter_Worker>("worker")
    .WithReference(postgres)
    .WithReference(rabbit).WaitFor(api);

builder.Build().Run();
