using Logic.Exceptions.Base;

namespace Logic.Exceptions.Tasks;

/// <summary>
/// Ошибка при не найденной задаче
/// </summary>
public class TaskNotFoundException : BaseNotFoundException
{
    public TaskNotFoundException(Guid id) : base("TaskNotFoundException", $"Задача с id - {id} не найдена")
    {

    }
}