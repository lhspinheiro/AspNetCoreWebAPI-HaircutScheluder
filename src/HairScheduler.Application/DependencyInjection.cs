using HairScheduler.Application.AutoMapper;
using HairScheduler.Application.UseCases.Schedules.Delete;
using HairScheduler.Application.UseCases.Schedules.GetAll;
using HairScheduler.Application.UseCases.Schedules.GetByNick;
using HairScheduler.Application.UseCases.Schedules.PDF;
using HairScheduler.Application.UseCases.Schedules.toSchudule;
using HairScheduler.Application.UseCases.Schedules.Update;
using Microsoft.Extensions.DependencyInjection;

namespace HairScheduler.Application;

public static class DependencyInjection
{

    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCase(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
    private static void AddUseCase(IServiceCollection services)
    {
        services.AddScoped<IScheduleUseCase, ScheduleUseCase>();
        services.AddScoped<IGetSchedulingByNickUseCase, GetSchedulingByNickUseCase>();
        services.AddScoped<IGetAllSchedules, GetAllSchedules>();
        services.AddScoped<IUpdateUseCase, UpdateUseCase>();
        services.AddScoped<IDeleteUseCase, DeleteUseCase>();
        services.AddScoped<IGeneratePdfUseCase, GeneratePdfUseCase>();
    }
}
