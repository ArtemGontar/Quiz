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
using AppQuiz.Application.Quizzes.Queries.GetAll;
using AppQuiz.Application.Quizzes.Queries.GetByChapterId;
using Microsoft.AspNetCore.Authorization;
using AppQuiz.Application.Chapters.Queries.GetByOwnerId;
using System.Security.Claims;
using OpenTracing;

namespace AppQuiz.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/chapters")]
    public class ChaptersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ChaptersController> _logger;
        private readonly ITracer _tracer;
        
        public ChaptersController(IMediator mediator, 
            ILogger<ChaptersController> logger,
            ITracer tracer)
        {
            _mediator = mediator;
            _logger = logger;
            _tracer = tracer;
        }

        [HttpGet]
        [SwaggerOperation("Get all chapters")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(IEnumerable<Chapter>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> GetAll()
        {
            using (var scope = _tracer.BuildSpan("GetAllChapters").StartActive(finishSpanOnDispose: true))
            {
                var response = await _mediator.Send(new GetAllChapterQuery());
                //catch if failure
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("byOwner")]
        [SwaggerOperation("Get all chapters by owner")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(IEnumerable<Chapter>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> GetAllByOwner()
        {
            using (var scope = _tracer.BuildSpan("GetAllChaptersByOwner").StartActive(finishSpanOnDispose: true))
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var response = await _mediator.Send(new GetChaptersByOwnerIdQuery() { OwnerId = userId });
                //catch if failure
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("{chapterId:guid}")]
        [SwaggerOperation("Get chapter by ID.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Chapter))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Chapter was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Get([FromRoute] Guid chapterId)
        {
            using (var scope = _tracer.BuildSpan("GetChapterById").StartActive(finishSpanOnDispose: true))
            {
                var response = await _mediator.Send(new GetChapterByIdQuery()
                {
                    ChapterId = chapterId
                });
                if (response == null)
                {
                    return NotFound($"Chapter with ID '{chapterId}' was not found.");
                }

                //catch if failure
                return Ok(response);   
            }
        }

        [HttpGet]
        [Route("{chapterId:guid}/quizzes")]
        [SwaggerOperation("Get quizzes by chapterId")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(IEnumerable<Quiz>))]
        public async Task<IActionResult> GetQuizzesByChapterId([FromRoute] Guid chapterId)
        {
            using (var scope = _tracer.BuildSpan("GetQuizzesByChapterId").StartActive(finishSpanOnDispose: true))
            {
                var response = await _mediator.Send(new GetQuizzesByChapterIdQuery()
                {
                    ChapterId = chapterId
                });
                //catch if failure
                return Ok(response);
            }
        }

        [HttpPost]
        [SwaggerOperation("Create chapter")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Create chapter failed")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Post([FromBody] CreateChapterCommand
            createChapterCommand)
        {
            using (var scope = _tracer.BuildSpan("CreateChapter").StartActive(finishSpanOnDispose: true))
            {
                try
                {
                    var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    createChapterCommand.OwnerId = userId;
                    var response = await _mediator.Send(createChapterCommand); 
                    return Ok(response);
                }
                catch(Exception e)
                {
                    return BadRequest(e);
                }
            }
            
        }

        [HttpPut]
        [Route("{chapterId:guid}")]
        [SwaggerOperation("Update chapter")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Chapter was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Put([FromRoute] Guid chapterId, [FromBody] UpdateChapterCommand updateChapterCommand)
        {
            using (var scope = _tracer.BuildSpan("UpdateChapter").StartActive(finishSpanOnDispose: true))
            {
                updateChapterCommand.SetId(chapterId);
                var response = await _mediator.Send(updateChapterCommand);
                //catch if failure
                return Ok(response);
            }
        }

        [HttpDelete]
        [Route("{chapterId:guid}")]
        [SwaggerOperation("Delete Chapter")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "chapter was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Delete([FromRoute] Guid chapterId)
        {
            using (var scope = _tracer.BuildSpan("DeleteChapter").StartActive(finishSpanOnDispose: true))
            {
                var response = await _mediator.Send(new DeleteChapterCommand(chapterId));
                //catch if failure
                return Ok(response);
            }
             
        }

    }
}
