var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var presentation = builder.AddProject<Projects.Presentation>("presentation");

builder.AddProject<Projects.Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(presentation);

builder.Build().Run();
