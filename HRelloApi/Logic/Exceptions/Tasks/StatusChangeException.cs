using System.Net;
using Logic.Exceptions.Base;

namespace Logic.Exceptions.Tasks;

/// <summary>
/// Ошибка при смене статута задачи
/// </summary>
public class StatusChangeException : BaseException
{
    public StatusChangeException() : base("StatusChangeException", "Ошибка при смене статуса задачи", 400)
    {
        
    }
}