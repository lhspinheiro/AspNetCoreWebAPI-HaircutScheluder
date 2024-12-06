using HairScheduler.Application.UseCases.Schedules.Delete;
using HairScheduler.Application.UseCases.Schedules.GetAll;
using HairScheduler.Application.UseCases.Schedules.GetByNick;
using HairScheduler.Application.UseCases.Schedules.toSchudule;
using HairScheduler.Application.UseCases.Schedules.Update;
using HairScheduler.Communication.Requests;
using HairScheduler.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace HairScheduler.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HairSchedulerController : ControllerBase
{
    // Realizar um agendamento
    [HttpPost]
    [ProducesResponseType(typeof(HairScheduleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Scheduling([FromServices] IScheduleUseCase useCase, [FromBody] RequestSchedule request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    // verificar agendamento especifico
    [HttpGet]
    [Route("{nickname}")]
    [ProducesResponseType(typeof(ResponseByNickname), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task <IActionResult> GetScheduling([FromServices] IGetSchedulingByNickUseCase useCase,  [FromRoute] string nickname)
    {
        var response = await useCase.Execute(nickname);

        return Ok(response);
    }

    // vericicar os agendamentos disponíveis. 
    [HttpGet]
    [ProducesResponseType(typeof(ResponseGetAll), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllSchedules([FromServices] IGetAllSchedules useCase)
    {
        var response = await useCase.Execute();

        if (response.Schedules.Count != 0)
            return Ok(response);    

        return NoContent();
    }

    // Atualizar o agendamento.
    [HttpPut]
    [Route("{nickname}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task <IActionResult> Update([FromServices] IUpdateUseCase useCase, [FromRoute] string nickname, [FromBody] RequestSchedule request)
    {
        await useCase.Execute(nickname, request);

        return NoContent();
    }

    //cancelar agendamento
    [HttpDelete]
    [Route("{nickname}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteUseCase useCase, [FromRoute] string nickname, DateTime date)
    {
        await useCase.Execute(nickname, date);

        return NoContent();
    }
}
