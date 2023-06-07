using Dal.Tasks.Entities;
using HRelloApi.Hubs.Tasks.Messages;
using Logic.Managers.Tasks.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace HRelloApi.Hubs.Tasks;

/// <summary>
/// Базовый хаб для уведомлений по задачам
/// </summary>
public class TaskHub : Hub
{
    private readonly ITaskUnitOfWorkManager _manager;
    
    
    public TaskHub(ITaskUnitOfWorkManager manager)
    {
        _manager = manager;
    }
    
    /// <summary>
    /// Метод об обновлении данных, получает обновленные данные и отсылает всем consumers сообщение, что задача обновлена
    /// </summary>
    public async Task SendNotificationUpdateTask(UpdateTaskMessage message)
    {
        var task = await _manager.GetAsync<TaskDal>(message.TaskId);
        
        // Отправка уведомления тому, кто изменил данные надо ли???
        await Clients.Caller.SendAsync("UpdateTaskNotify", task?.Id);
        // Отправка уведомления всем остальным, кто подписан на задачу (пока только тот, кто делает задачу)
        await Clients.Others.SendAsync("UpdateTaskReceive", $"{task?.Name} {message}");
    }
}