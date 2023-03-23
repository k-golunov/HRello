using Dal.Base.Entitities;

namespace Dal.Tasks.Entities;

public class HistoryDal : BaseDal<Guid>
{
    /// <summary>
    /// 
    /// </summary>
    public string ActionType { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Comment { get; set; }
}