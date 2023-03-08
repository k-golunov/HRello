using Dal.Base.Interfaces;
using Dal.Email.Entities;

namespace Dal.Email.Interfaces;

/// <summary>
/// Пример использования базового интерфейса репозитория
/// возможно потребуется для авторизации по Email
/// </summary>
public interface IEmailRepository : IBaseRepository<EmailDal, int>
{
    
}