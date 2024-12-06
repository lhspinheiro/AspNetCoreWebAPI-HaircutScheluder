using HairScheduler.Communication.Responses;
using HairScheduler.Exception;
using HairScheduler.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HairScheduler.Api.Filter;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is HairSchedulerException)
        {
            Exceptions(context);
        }
        /* else
        {

            ThrowUnkowError(context);
        }*/
    }
    private void Exceptions (ExceptionContext context)
    {
        var cashflowException = (HairSchedulerException)context.Exception;
        var errorResponse = new ResponseErrorJson(cashflowException.GetErros());

        context.HttpContext.Response.StatusCode = cashflowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }


     private void ThrowUnkowError(ExceptionContext context)
     {
         var errorResponse = new ResponseErrorJson(ResourceErrors.UNKNOW_ERROR);

         context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
         context.Result = new ObjectResult(errorResponse);
     }
}
