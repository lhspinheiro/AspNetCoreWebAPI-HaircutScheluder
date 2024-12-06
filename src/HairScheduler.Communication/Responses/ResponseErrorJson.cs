namespace HairScheduler.Communication.Responses;

public class ResponseErrorJson
{
    public List<string> ErrorrMessages { get; set; }


    public ResponseErrorJson(string errorMessage)
    {
        ErrorrMessages = [errorMessage];
    }

    public ResponseErrorJson(List<string> errorMessage)
    {
        ErrorrMessages = errorMessage;
    }
}

