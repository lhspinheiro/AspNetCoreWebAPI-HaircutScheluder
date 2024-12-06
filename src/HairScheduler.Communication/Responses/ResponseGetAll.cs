using HairScheduler.Communication.Enums;

namespace HairScheduler.Communication.Responses;

public class ResponseGetAll
{
    public List<ResponseShortGetAll> Schedules { get; set; } = [];


}
