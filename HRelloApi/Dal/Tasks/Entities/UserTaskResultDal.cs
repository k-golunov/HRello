using Dal.Base.Entitities;

namespace Dal.Tasks.Entities;

public class UserTaskResultDal : BaseDal<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public int FactResult { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Result { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public int FactWeight { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Guid TaskId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public TaskDal Task { get; set; }
}