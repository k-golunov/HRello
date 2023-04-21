using Dal.Tasks.Entities;

namespace HRelloApi.Controllers.Public.Tasks.dto.response;


/// <summary>
/// Модель ответа на запрос получение всех задач с фильтрами по страницам 
/// </summary>
public class AllTasksResponse
{
    /// <summary>
    /// Общее количество задач
    /// </summary>
    public int AllTasksCount { get; init; }
    
    /// <summary>
    /// Задачи
    /// </summary>
    public List<TaskDal> Tasks { get; init; }

    public AllTasksResponse(int allTasksCount, List<TaskDal> tasks)
    {
        AllTasksCount = allTasksCount;
        Tasks = tasks;
    }
}