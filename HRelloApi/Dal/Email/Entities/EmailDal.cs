using Dal.Base.Entitities;

namespace Dal.Email.Entities;

public class EmailDal : BaseDal<int>
{
    public string Email { get; set; }
}