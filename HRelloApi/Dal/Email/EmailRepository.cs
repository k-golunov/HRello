using Dal.Base;
using Dal.Email.Entities;
using Dal.Email.Interfaces;

namespace Dal.Email;

/// <summary>
/// Пример использования базового репозитория
/// возможно потребуется для авторизации по Email
/// </summary>
public class EmailRepository : BaseRepository<EmailDal, int>, IEmailRepository
{
    public EmailRepository(DataContext context) : base(context)
    {
    }
}