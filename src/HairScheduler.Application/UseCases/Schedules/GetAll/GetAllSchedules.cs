using AutoMapper;
using HairScheduler.Communication.Responses;
using HairScheduler.Domain.Repositories.Interfaces;

namespace HairScheduler.Application.UseCases.Schedules.GetAll;

public class GetAllSchedules : IGetAllSchedules
{

    private readonly IReadOnlyRepository _repository;
    private readonly IMapper _mapper;



    public GetAllSchedules(IReadOnlyRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ResponseGetAll> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseGetAll
        {
            Schedules = _mapper.Map<List<ResponseShortGetAll>>(result)
        };
    }
}
