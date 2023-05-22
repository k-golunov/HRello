namespace Logic.Exceptions.Base;

/// <summary>
/// базовый эксепщен для 404
/// </summary>
public abstract class BaseNotFoundException: BaseException
{
    public BaseNotFoundException(string code, string message) : base("Not Found", message, 404)
    {
        
    }
}