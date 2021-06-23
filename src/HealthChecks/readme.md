# Health checks

Package used: https://www.nuget.org/packages/Microsoft.Extensions.Diagnostics.HealthChecks

To begin, you need to define what constitutes a healthy status for each microservice. In the sample application, we define the microservice is healthy if its API is accessible via HTTP and its related SQL Server database is also available.

Microservices in eShopOnContainers rely on multiple services to perform its task. For example, the `Catalog.API` microservice from eShopOnContainers depends on many services, such as Azure Blob Storage, SQL Server, and RabbitMQ. Therefore, it has several health checks added using the `AddCheck()` method. For every dependent service, a custom `IHealthCheck` implementation that defines its respective health status would need to be added.
The open-source project [↑ AspNetCore.Diagnostics.HealthChecks](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks) solves this problem by providing custom health check implementations for each of these enterprise services, that are built on top of .NET 5. Each health check is available as an individual NuGet package that can be easily added to the project.

