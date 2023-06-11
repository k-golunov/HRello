using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Dal.Base.Entitities;
using Dal.Entities;
using Dal.TaskResult.Entities;
using Dal.Tasks.Enum;
using Dal.User.Models;

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
    
    /// <summary>
    /// Идентификатор отдела пользователя
    /// </summary>
    public int DepartamentId { get; set; }
    
    /// <summary>
    /// Поле, нужное для создания миграции через EF core
    /// Хранит отдел пользователя и все данные по нему
    /// </summary>
    public DepartamentDal Departament { get; set; }

    /// <summary>
    /// Итоги от сотрудника
    /// </summary>
    public UserTaskResultDal? UserTaskResultDal { get; set; }
    
    /// <summary>
    /// Итоги от руководителя
    /// </summary>
    public BossTaskResultDal? BossTaskResultDal { get; set; }
    
    /// <summary>
    /// итоги от руководителя, к которым привязанна задача
    /// </summary>
    public List<TaskResultDal> TaskResults { get; set; }
    
    /// <summary>
    /// итог по задаче от сотрудника
    /// </summary>
    public UserTaskResultDal? UserResult { get; set; }
    
    /// <summary>
    /// итог по задаче от руководителя
    /// </summary>
    public BossTaskResultDal? BossResult { get; set; }
}