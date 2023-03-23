using Dal.Base.Entitities;

namespace Dal.Tasks.Entities;

public class BossTaskResultDal : BaseDal<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public int Result { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Comment { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Guid TaskId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public TaskDal Task { get; set; }
}