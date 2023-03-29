using System.ComponentModel.DataAnnotations;
using Dal.Base.Entitities;

namespace Dal.Tasks.Entities;

public class BossTaskResultDal : BaseDal<Guid>
{
    /// <summary>
    /// Результат от руководителя в процентах
    /// </summary>
    [Range(0,1000)]
    public int Result { get; set; }
    
    /// <summary>
    /// Комментарий к исполнению задачи от руководителя
    /// </summary>
    [MaxLength(255)]
    public string? Comment { get; set; }
    
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid TaskId { get; set; }
    
    /// <summary>
    /// Сама задача
    /// </summary>
    public TaskDal Task { get; set; }
}