using AppQuiz.Application.Questions.Commands.Create;
using AppQuiz.Application.Questions.Commands.Delete;
using AppQuiz.Application.Questions.Commands.Update;
using AppQuiz.Application.Questions.Queries.GetAll;
using AppQuiz.Application.Questions.Queries.GetById;
using AppQuiz.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTracing;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AppQuiz.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionsController> _logger;
        private readonly ITracer _tracer;

        public QuestionsController(IMediator mediator, 
            ILogger<QuestionsController> logger,
            ITracer tracer)
        {
            _mediator = mediator;
            _logger = logger;
            _tracer = tracer;
        }

        [HttpGet]
        [SwaggerOperation("Get all questions")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(IEnumerable<Question>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> GetAll()
        {
            using (var scope = _tracer.BuildSpan("GetAllQuestions").StartActive(finishSpanOnDispose: true))
            {
                var response = await _mediator.Send(new GetAllQuestionQuery());
                //catch if failure
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("{questionId:guid}")]
        [SwaggerOperation("Get question by ID.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Question))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Question was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Get([FromQuery] Guid questionId)
        {
            using (var scope = _tracer.BuildSpan("GetQuestionById").StartActive(finishSpanOnDispose: true))
            {
                var response = await _mediator.Send(new GetQuestionByIdQuery()
                {
                    QuestionId = questionId
                });
                if (response == null)
                {
                    return NotFound($"Question with ID '{questionId}' was not found.");
                }

                //catch if failure
                return Ok(response);

            }
        }

        [HttpPost]
        [SwaggerOperation("Create question")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Post([FromBody] CreateQuestionCommand createQuestionCommand)
        {
            using (var scope = _tracer.BuildSpan("CreateQuestion").StartActive(finishSpanOnDispose: true))
            {
                var response = await _mediator.Send(createQuestionCommand);

                //catch if failure
                return Ok(response);
            }             
        }

        [HttpPut]
        [Route("{questionId:guid}")]
        [SwaggerOperation("Update question")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Question was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Put([FromRoute] Guid questionId, [FromBody] UpdateQuestionCommand updateQuestionCommand)
        {
            using (var scope = _tracer.BuildSpan("UpdateQuestion").StartActive(finishSpanOnDispose: true))
            {
                updateQuestionCommand.SetQuestionId(questionId);
                var response = await _mediator.Send(updateQuestionCommand);

                //catch if failure
                return Ok(response);
            }
        }

        [HttpDelete]
        [Route("{questionId:guid}")]
        [SwaggerOperation("Delete question")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Question was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Delete([FromRoute] Guid questionId)
        {
            var scope = _tracer.BuildSpan("DeleteQuestion").Start();
            var response = await _mediator.Send(new DeleteQuestionCommand(questionId));
            scope.Finish();
            //catch if failure
            return Ok(response);
        }
    }
}
