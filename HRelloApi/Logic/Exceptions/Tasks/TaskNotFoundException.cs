using Logic.Exceptions.Base;

namespace Logic.Exceptions.Tasks;

public class TaskNotFoundException : BaseException
{
    public TaskNotFoundException() : base("TaskNotFoundException", "Пользователь не найден", 404)
    {

    }
}