using Dal.TaskResult.Entities;
using Dal.TaskResult.Repositories;
using Dal.TaskResult.Repositories.Interfaces;
using HRelloApi.Controllers.Public.Results.mapping;
using Logic.Managers.Result;
using Logic.Managers.Result.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HRelloApi.ProgramExtension;

public static class TaskResultExtension
{
    public static IServiceCollection AddTasksResults(this IServiceCollection services)
    {
        services.TryAddScoped<ITaskResultRepository, TaskResultRepository>();
        services.TryAddScoped<ITaskResultManager, TaskResultManager>();
        services.AddAutoMapper(typeof(TaskResultMapping));

        return services;
    }
}