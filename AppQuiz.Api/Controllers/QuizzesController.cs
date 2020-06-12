using AppQuiz.Application.Quizzes.Commands.Create;
using AppQuiz.Application.Quizzes.Commands.Delete;
using AppQuiz.Application.Quizzes.Commands.Update;
using AppQuiz.Application.Quizzes.Queries.GetAll;
using AppQuiz.Application.Quizzes.Queries.GetById;
using AppQuiz.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AppQuiz.Application.Questions.Queries.GetByQuizId;
using AppQuiz.Application.Quizzes.Commands.Result;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using OpenTracing;

namespace AppQuiz.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/quizzes")]
    public class QuizzesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuizzesController> _logger;
        private readonly ITracer _tracer; 
        public QuizzesController(IMediator mediator, 
            ILogger<QuizzesController> logger,
            ITracer tracer)
        {
            _mediator = mediator;
            _logger = logger;
            _tracer = tracer;
        }

        [HttpGet]
        [SwaggerOperation("Get all quizzes")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(IEnumerable<Quiz>))]
        public async Task<IActionResult> GetAll()
        {
            using (var scope = _tracer.BuildSpan("GetAllQuizzes").StartActive(finishSpanOnDispose: true))
            {
                var response = await _mediator.Send(new GetAllQuizQuery());
                //catch if failure
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("{quizId:guid}")]
        [SwaggerOperation("Get quiz by ID.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Quiz))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Quiz was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Get([FromQuery] GetQuizByIdQuery query)
        {
            using (var scope = _tracer.BuildSpan("GetQuizById").StartActive(finishSpanOnDispose: true))
            {
                var response = await _mediator.Send(new GetQuizByIdQuery());
                if (response == null)
                {
                    return NotFound($"Question with ID '{query.QuizId}' was not found.");
                }
                //catch if failure
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("{quizId:guid}/questions")]
        [SwaggerOperation("Get questions by quizId")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(IEnumerable<Quiz>))]
        public async Task<IActionResult> GetQuestionsByQuizId([FromRoute] Guid quizId)
        {
            using (var scope = _tracer.BuildSpan("GetQuestionsByQuizId").StartActive(finishSpanOnDispose: true))
            {
                var response = await _mediator.Send(new GetQuestionsByQuizIdQuery()
                {
                    QuizId = quizId
                });
                //catch if failure
                return Ok(response);
            }
        }

        [HttpPost]
        [SwaggerOperation("Create quiz")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Post([FromBody] CreateQuizCommand createQuizCommand)
        {
            using (var scope = _tracer.BuildSpan("CreateQuiz").StartActive(finishSpanOnDispose: true))
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                createQuizCommand.OwnerId = userId;
                var response = await _mediator.Send(createQuizCommand);
                //catch if failure
                return Ok(response);
            }
        }

        [HttpPut]
        [Route("{quizId:guid}")]
        [SwaggerOperation("Update quiz")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Quiz was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Put([FromRoute] Guid quizId, [FromBody] UpdateQuizCommand updateQuizCommand)
        {
            using (var scope = _tracer.BuildSpan("UpdateQuiz").StartActive(finishSpanOnDispose: true))
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                updateQuizCommand.SetIdAndOwnerId(quizId, userId);
                var response = await _mediator.Send(updateQuizCommand);
                //catch if failure
                return Ok(response);
            }
        }

        [HttpDelete]
        [Route("{quizId:guid}")]
        [SwaggerOperation("Delete quiz")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Quiz was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Delete([FromRoute] Guid quizId)
        {
            using (var scope = _tracer.BuildSpan("DeleteQuiz").StartActive(finishSpanOnDispose: true))
            {
                var response = await _mediator.Send(new DeleteQuizCommand(quizId));
                //catch if failure
                return Ok(response);
            }
        }

        [HttpPost]
        [Route("{quizId:guid}/result")]
        [SwaggerOperation("Quiz result")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> PostResult([FromRoute] Guid quizId, [FromBody] IEnumerable<string> answers)
        {
            using (var scope = _tracer.BuildSpan("QuizResult").StartActive(finishSpanOnDispose: true))
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var response = await _mediator.Send(new ResultQuizCommand(quizId, userId)
                {
                    Answers = answers
                });
                //catch if failure
                return Ok(response);
            }
        }
    }
}
