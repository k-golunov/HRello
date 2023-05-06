using Logic.Exceptions.Base;

namespace Logic.Exceptions.Tasks;

public class WrongUrlForChangeStatusException: BaseException
{
    public WrongUrlForChangeStatusException(): base("WrongUrlForChangeStatusException","Use a different url for this status change", 400)
    {
        
    }
}