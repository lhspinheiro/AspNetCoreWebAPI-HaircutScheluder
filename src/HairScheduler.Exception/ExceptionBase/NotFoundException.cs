using System.Net;

namespace HairScheduler.Exception.ExceptionBase;

public class NotFoundException : HairSchedulerException
{


    public NotFoundException(string message) : base(message)
    {
        
    }
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErros()
    {
        return [Message];
    }
}
