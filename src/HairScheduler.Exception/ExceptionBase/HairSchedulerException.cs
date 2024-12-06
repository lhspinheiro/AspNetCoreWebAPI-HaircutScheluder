namespace HairScheduler.Exception.ExceptionBase;

public abstract class HairSchedulerException : SystemException
{

    protected HairSchedulerException(string message) : base(message)
    {
    }
    public abstract int StatusCode { get; }

    public abstract List<string> GetErros();

}
