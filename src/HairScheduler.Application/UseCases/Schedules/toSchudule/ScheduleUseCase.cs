using AutoMapper;
using HairScheduler.Communication.Requests;
using HairScheduler.Communication.Responses;
using HairScheduler.Domain.Entities;
using HairScheduler.Domain.Repositories;
using HairScheduler.Domain.Repositories.Interfaces;
using HairScheduler.Exception.ExceptionBase;
using System;

namespace HairScheduler.Application.UseCases.Schedules.toSchudule;
public class ScheduleUseCase : IScheduleUseCase
{

    private readonly IWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public ScheduleUseCase(IWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<HairScheduleResponse> Execute(RequestSchedule request)
    {
        Validate(request);

        var entity = _mapper.Map<Schedule>(request);

        await _repository.Add(entity);
        await _unitOfWork.Commit();

        return new HairScheduleResponse
        {
            Information = "Agendado realizado com sucesso!"
        };
    } 

    private void Validate(RequestSchedule request)
    {
        var validator = new Validator();
        var result  = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);        
        }
    }
}
