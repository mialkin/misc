# Health checks

Package used: https://www.nuget.org/packages/Microsoft.Extensions.Diagnostics.HealthChecks

To begin, you need to define what constitutes a healthy status for each microservice. In the sample application, we define the microservice is healthy if its API is accessible via HTTP and its related SQL Server database is also available.

Microservices in eShopOnContainers rely on multiple services to perform its task. For example, the `Catalog.API` microservice from eShopOnContainers depends on many services, such as Azure Blob Storage, SQL Server, and RabbitMQ. Therefore, it has several health checks added using the `AddCheck()` method. For every dependent service, a custom `IHealthCheck` implementation that defines its respective health status would need to be added.
The open-source project [↑ AspNetCore.Diagnostics.HealthChecks](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks) solves this problem by providing custom health check implementations for each of these enterprise services, that are built on top of .NET 5. Each health check is available as an individual NuGet package that can be easily added to the project.

## Kubernetes liveness and readiness probes

Kubernetes uses **liveness** probes to know when to restart a container. If a container is unresponsive—perhaps the application is deadlocked due to a multi-threading defect—restarting the container can make the application more available, despite the defect. It certainly beats paging someone in the middle of the night to restart a container.

Kubernetes uses **readiness** probes to decide when the container is available for accepting traffic. The readiness probe is used to control which pods are used as the backends for a service. A pod is considered ready when all of its containers are ready. If a pod is not ready, it is removed from service load balancers. For example, if a container loads a large cache at startup and takes minutes to start, you do not want to send requests to this container until it is ready, or the requests will fail—you want to route requests to other pods, which are capable of servicing requests.

At the time of this writing, Kubernetes supports three mechanisms for implementing liveness and readiness probes: 1) running a command inside a container, 2) making an HTTP request against a container, or 3) opening a TCP socket against a container.

A probe has a number of configuration parameters to control its behaviour, like how often to execute the probe; how long to wait after starting the container to initiate the probe; the number of seconds after which the probe is considered failed; and how many times the probe can fail before giving up. For a liveness probe, giving up means the pod will be restarted. For a readiness probe, giving up means not routing traffic to the pod, but the pod is not restarted. Liveness and readiness probes can be used in conjunction.

[↑ Kubernetes Liveness and Readiness Probes: How to Avoid Shooting Yourself in the Foot](https://blog.colinbreck.com/kubernetes-liveness-and-readiness-probes-how-to-avoid-shooting-yourself-in-the-foot)
