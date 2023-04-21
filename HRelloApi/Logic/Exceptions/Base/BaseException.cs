namespace Logic.Exceptions.Base;

public class BaseException : Exception
{
    public string Code { get; set; }
    public int Status { get; set; }
    
    public BaseException() : base()
    {
        
    }
    
    public BaseException(string code, string message, int status) : base(message)
    {
        Code = code;
        Status = status;
    }
}