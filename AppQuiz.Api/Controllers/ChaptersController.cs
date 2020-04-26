using AppQuiz.Application.Chapters.Commands.Create;
using AppQuiz.Application.Chapters.Commands.Delete;
using AppQuiz.Application.Chapters.Commands.Update;
using AppQuiz.Application.Chapters.Queries.GetAll;
using AppQuiz.Application.Chapters.Queries.GetById;
using AppQuiz.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AppQuiz.Api.Controllers
{
    [ApiController]
    [Route("chapters")]
    public class ChaptersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ChaptersController> _logger;

        public ChaptersController(IMediator mediator, ILogger<ChaptersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation("Get all chapters")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(IEnumerable<Chapter>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllChapterQuery());
            //catch if failure
            return Ok(response);
        }

        [HttpGet]
        [Route("{chapterId:guid}")]
        [SwaggerOperation("Get chapter by ID.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Chapter))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Chapter was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Get([FromQuery] GetChapterByIdQuery query)
        {
            var response = await _mediator.Send(query);
            if (response == null)
            {
                return NotFound($"Chapter with ID '{query.ChapterId}' was not found.");
            }

            //catch if failure
            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation("Create chapter")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Post([FromBody] CreateChapterCommand
            createChapterCommand)
        {
            var response = await _mediator.Send(createChapterCommand);
            //catch if failure
            return Ok(response);
        }

        [HttpPut]
        [Route("{chapterId:guid}")]
        [SwaggerOperation("Update chapter")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Chapter was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Put([FromRoute] Guid chapterId, [FromBody] UpdateChapterCommand updateChapterCommand)
        {
            updateChapterCommand.SetId(chapterId);
            var response = await _mediator.Send(updateChapterCommand);
            //catch if failure
            return Ok(response);
        }

        [HttpDelete]
        [Route("{chapterId:guid}")]
        [SwaggerOperation("Delete Chapter")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "chapter was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Delete([FromRoute] Guid chapterId)
        {
            var response = await _mediator.Send(new DeleteChapterCommand(chapterId));
            //catch if failure
            return Ok(response);
        }

    }
}
