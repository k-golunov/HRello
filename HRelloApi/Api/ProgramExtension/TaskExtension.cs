using Dal.Tasks.Repositories;
using Dal.Tasks.Repositories.Interfaces;
using Logic.Managers.Tasks;
using Logic.Managers.Tasks.Interfaces;
using Logic.Managers.Tasks.StatusesTree;

namespace HRelloApi.ProgramExtension;

public static class TaskExtension
{
    public static IServiceCollection AddTasks(this IServiceCollection services)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddSingleton<StatusTree>();
        services.AddScoped<ITaskUnitOfWorkManager, TaskUnitOfWorkManager>();
        services.AddScoped<IHistoryRepository, HistoryRepository>();
        services.AddScoped<IBossTaskResultsRepository, BossTaskResultsRepository>();
        services.AddScoped<IUserTaskResultsRepository, UserTaskResultsRepository>();
        
        return services;
    }
}