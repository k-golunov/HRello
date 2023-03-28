using System.ComponentModel.DataAnnotations;
using Dal.Base.Entitities;
using Dal.Entities;
using Dal.Tasks.Enum;

namespace Dal.Tasks.Entities;

/// <summary>
/// 
/// </summary>
public class TaskDal : BaseDal<Guid>
{
    /// <summary>
    /// Название задачи
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Год создания задачи
    /// </summary>
    public int Year { get; set; }
    
    /// <summary>
    /// Квартал задачи
    /// </summary>
    [Range(1,4)]
    public int Quarter { get; set; }
    
    /// <summary>
    /// Категория задачи
    /// </summary>
    public string Category { get; set; }
    
    /// <summary>
    /// Блок задачи
    /// </summary>
    public string Block { get; set; }
    
    /// <summary>
    /// Планируемый вес задачи
    /// </summary>
    public int PlannedWeight { get; set; }
    
    /// <summary>
    /// Ожидаемый результат
    /// </summary>
    public string WaitResult { get; set; }
    
    /// <summary>
    /// Текущий статус
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Сотрудник, выполняющий задачу
    /// </summary>
    public UserDal User { get; set; }
    
    /// <summary>
    /// История изменений
    /// </summary>
    public List<HistoryDal> History { get; set; }
}