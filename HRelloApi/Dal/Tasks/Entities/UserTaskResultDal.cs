using System.ComponentModel.DataAnnotations;
using Dal.Base.Entitities;

namespace Dal.Tasks.Entities;

public class UserTaskResultDal : BaseDal<Guid>
{
    /// <summary>
    /// Фактический результат, в процентах
    /// </summary>
    [Range(0, 1000)]
    public int FactResult { get; set; }
    
    /// <summary>
    /// Описание достигнутого результата
    /// </summary>
    [MaxLength(1023)]
    public string Result { get; set; }
    
    /// <summary>
    /// Комментарий от сотрудника к завершению задачи
    /// </summary>
    [MaxLength(255)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Фактический вес
    /// </summary>
    [Range(0, 1000)]
    public int FactWeight { get; set; }
    
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid TaskId { get; set; }
    
    /// <summary>
    /// Задача
    /// </summary>
    public TaskDal Task { get; set; }
}