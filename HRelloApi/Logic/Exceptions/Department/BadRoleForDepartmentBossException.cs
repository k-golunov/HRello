using Logic.Exceptions.Base;

namespace Logic.Exceptions.Department;

public class BadRoleForDepartmentBossException: BaseException
{
    public BadRoleForDepartmentBossException(): base("BadRoleForDepartmentBossException", "Роль пользователя не походит для руководителя отдела", 400)
    {
        
    }
}