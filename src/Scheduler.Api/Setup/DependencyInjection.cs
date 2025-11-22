using Scheduler.Application.Appointements;
using Scheduler.Application.Clients;
using Scheduler.Application.Patients;
using Scheduler.Data.Appointements.Repositories;
using Scheduler.Data.Clients.Repositories;
using Scheduler.Data.Patients.Repositories;

namespace Scheduler.Api.Setup;

public static class DependencyInjection
{
    public static void AddSchedulerApiServices(this IServiceCollection services)
    {
        services.AddSingleton<IClientRepository, InMemoryClientRepository>();
        services.AddSingleton<IClientService, ClientService>();

        services.AddSingleton<IPatientRepository, InMemoryPatientRepository>();
        services.AddSingleton<IPatientService, PatientService>();

        services.AddSingleton<IAppointementRepository, InMemoryAppointementRepository>();
        services.AddSingleton<IAppointementService, AppointementService>();
    }
}
