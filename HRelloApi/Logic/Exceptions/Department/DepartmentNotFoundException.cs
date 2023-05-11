using Logic.Exceptions.Base;

namespace Logic.Exceptions.Department;

/// <summary>
/// Ошибка при не найденном отделе
/// </summary>
public class DepartmentNotFoundException: BaseNotFoundException
{
    public DepartmentNotFoundException(int id): base("DepartmentNotFoundException", $"Отдел с id - {id} не найден")
    {
        
    }
}