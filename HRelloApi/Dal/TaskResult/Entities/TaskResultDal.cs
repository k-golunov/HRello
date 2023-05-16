using Dal.Base.Entitities;
using Dal.TaskResult.Enums;
using Dal.Tasks.Entities;

namespace Dal.TaskResult.Entities;

/// <summary>
/// Итоги от руководителя по нескольким задачам
/// </summary>
public class TaskResultDal : BaseDal<Guid>
{
    /// <summary>
    /// Текстовый итог
    /// </summary>
    public string Result { get; set; }
    
    /// <summary>
    /// Цвет задачи
    /// </summary>
    public ColorEnum Color { get; set; }

    /// <summary>
    /// Связанные задачи 
    /// </summary>
    public List<TaskDal> Tasks { get; set; } = new();
}