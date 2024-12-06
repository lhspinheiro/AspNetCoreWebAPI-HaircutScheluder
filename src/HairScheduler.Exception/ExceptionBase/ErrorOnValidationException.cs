using System.Net;

namespace HairScheduler.Exception.ExceptionBase;

public class ErrorOnValidationException : HairSchedulerException
{
    private readonly List<string> _erros;


    public override int StatusCode => (int)HttpStatusCode.BadRequest;


   public ErrorOnValidationException(List<string> errosMessages) : base(string.Empty)
    {
        _erros = errosMessages;
    }

    public override List<string> GetErros()
    {
        return _erros;
    }

}
