using System.ComponentModel.DataAnnotations;
using Dal.Base.Entitities;
using Dal.Tasks.Enum;

namespace Dal.Tasks.Entities;

public class HistoryDal : BaseDal<Guid>
{
    /// <summary>
    /// Тип изменения
    /// </summary>
    public ActionTypeEnum ActionType { get; set; }
    
    /// <summary>
    /// Дата изменения
    /// </summary>
    [Required]
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Комментарий к изменению
    /// </summary>
    public string? Comment { get; set; }
    
    /// <summary>
    /// Задача
    /// </summary>
    public TaskDal Task { get; set; }
    
    /// <summary>
    /// идентификатор пользователя, который внес изменения
    /// </summary>
    public string UserId { get; set; }

    public HistoryDal()
    {
        
    }
    
    public HistoryDal(ActionTypeEnum type, TaskDal taskDal, string userId, string? comment = null)
    {
        Id = Guid.NewGuid();
        ActionType = type;
        Date = DateTime.UtcNow;
        Comment = comment;
        Task = taskDal;
        UserId = userId;
    }
}