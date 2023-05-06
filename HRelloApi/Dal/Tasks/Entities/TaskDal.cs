using System.ComponentModel;
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
    [MaxLength(255)]
    public string Name { get; set; }
    
    /// <summary>
    /// Год задачи
    /// </summary>
    [MaxLength(4)]
    [MinLength(4)]
    public int Year { get; set; }
    
    /// <summary>
    /// Квартал задачи, от 1 до 4
    /// </summary>
    [Range(1,4)]
    public int Quarter { get; set; }
    
    /// <summary>
    /// Категория задачи enum
    /// </summary>
    public CategoryEnum Category { get; set; }
    
    /// <summary>
    /// Блок задачи enum
    /// </summary>
    public BlockDal Block { get; set; }
    
    /// <summary>
    /// Планируемый вес задачи, предполагаектся значение процента
    /// Например если значение 50, то это означает, что вес 50%
    /// </summary>
    [Range(0, 1000)]
    public int PlannedWeight { get; set; }
    
    /// <summary>
    /// Ожидаемый результат по итогам задачи
    /// </summary>
    [MaxLength(1023)]
    public string WaitResult { get; set; }
    
    /// <summary>
    /// Статус задачи
    /// </summary>
    public StatusEnum Status { get; set; }

    /// <summary>
    /// Сотрудник, отвественный за задачу
    /// </summary>
    public UserDal User { get; set; }

    /// <summary>
    /// История изменений задачи
    /// </summary>
    public List<HistoryDal> History { get; set; } = new();
}