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
    /// 
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int Year { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Range(1,4)]
    public int Quarter { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Category { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Block { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int PlannedWeight { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string WaitResult { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public StatusEnum Status { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public UserDal User { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public List<HistoryDal> History { get; set; }
}