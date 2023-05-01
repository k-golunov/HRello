using System.Net;
using Logic.Exceptions.Base;

namespace Logic.Exceptions.Tasks;

public class StatusChangeException : BaseException
{
    public StatusChangeException() : base("StatusChangeException", "Ошибка при смене статуса задачи", 400)
    {
        
    }
}