using AutoMapper;
using HairScheduler.Communication.Requests;
using HairScheduler.Domain.Repositories;
using HairScheduler.Domain.Repositories.Interfaces;
using HairScheduler.Exception;
using HairScheduler.Exception.ExceptionBase;

namespace HairScheduler.Application.UseCases.Schedules.Update;

public class UpdateUseCase : IUpdateUseCase
{
    private readonly IUpdateOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;


    public UpdateUseCase(IUpdateOnlyRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _repository = repository;   
        _unitOfWork = unitOfWork;
    }


    public async Task Execute(string nickname, RequestSchedule request)
    {
        Validate(request);


        var verify = await _repository.GetByNickname(nickname);

        if ( verify == null)
        {
            throw new NotFoundException(ResourceErrors.NICKNAME_NOT_FOUND);
        }

        _mapper.Map(request, verify);
        _repository.Update(verify);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestSchedule request)
    {
        var validator = new Validator();
        var result = validator.Validate(request);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();


            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
