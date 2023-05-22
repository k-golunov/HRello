using Dal.Results.Repositories;
using Dal.Results.Repositories.Interfaces;
using HRelloApi.Controllers.Public.Results.mapping;
using Logic.Managers.Result;
using Logic.Managers.Result.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HRelloApi.ProgramExtension;

public static class TaskResultExtension
{
    public static IServiceCollection AddTasksResults(this IServiceCollection services)
    {
        services.TryAddScoped<IResultRepository, ResultRepository>();
        services.TryAddScoped<IResultManager, ResultManager>();
        services.AddAutoMapper(typeof(ResultMapping));

        return services;
    }
}