using AutoMapper;
using HairScheduler.Communication.Requests;
using HairScheduler.Communication.Responses;
using HairScheduler.Domain.Entities;

namespace HairScheduler.Application.AutoMapper;

public class AutoMapping : Profile 
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestSchedule, Schedule>();
    }
    private void EntityResponse()
    {
        CreateMap<Schedule, HairScheduleResponse>();
        CreateMap<Schedule, ResponseByNickname>();
        CreateMap<Schedule, ResponseShortGetAll>();
    }

}
