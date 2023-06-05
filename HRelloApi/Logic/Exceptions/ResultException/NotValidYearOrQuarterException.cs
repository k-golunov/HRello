using Logic.Exceptions.Base;

namespace Logic.Exceptions.TaskResultException;

public class NotValidYearOrQuarterException : BaseException
{
    public NotValidYearOrQuarterException(int year, int quarter) : base("NotValidYearOrQuarterException",
        $"квартал: {quarter} и/или год {year} не совпадает с выбранными задачами", 400)
    {
        
    }
}