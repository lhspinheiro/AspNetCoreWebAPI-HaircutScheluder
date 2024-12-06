using AutoMapper;
using HairScheduler.Communication.Responses;
using HairScheduler.Domain.Repositories.Interfaces;
using HairScheduler.Exception;
using HairScheduler.Exception.ExceptionBase;

namespace HairScheduler.Application.UseCases.Schedules.GetByNick;

public class GetSchedulingByNickUseCase : IGetSchedulingByNickUseCase
{

    private readonly IReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetSchedulingByNickUseCase(IReadOnlyRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ResponseByNickname> Execute(string nickname)
    {
        var result = await _repository.GetByNick(nickname);
       
        if (result is null)
        {
            throw new NotFoundException(ResourceErrors.NICKNAME_NOT_FOUND);
        }

        return _mapper.Map<ResponseByNickname>(result);      
    }
}
