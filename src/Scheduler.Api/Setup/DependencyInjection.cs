using Scheduler.Application.Clients;
using Scheduler.Data.Clients.Repositories;

namespace Scheduler.Api.Setup;

public static class DependencyInjection
{
    public static void AddSchedulerApiServices(this IServiceCollection services)
    {
        services.AddSingleton<IClientRepository, InMemoryClientRepository>();
        services.AddSingleton<IClientService, ClientService>();
    }
}
