using HairScheduler.Application.UseCases.Schedules.PDF;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace HairScheduler.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GeneratorPDFInfoController : ControllerBase
{

    [HttpGet]
    [Route("{nickname}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task <IActionResult> GeneratePdf([FromServices] IGeneratePdfUseCase useCase, [FromQuery] DateTime day, [FromRoute] string nickname)
    {
        byte[] file = await useCase.Execute(day, nickname);

        if (file.Length > 0)
            return File (file, MediaTypeNames.Application.Pdf, "Informations.pdf");

        return NoContent();
    }

}
