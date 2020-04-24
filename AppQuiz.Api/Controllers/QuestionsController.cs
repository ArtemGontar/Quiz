using AppQuiz.Application.Questions.Queries.GetAll;
using AppQuiz.Application.Questions.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AppQuiz.Application.Questions.Commands.Create;
using AppQuiz.Application.Questions.Commands.Delete;
using AppQuiz.Application.Questions.Commands.Update;
using AppQuiz.Domain;
using Swashbuckle.AspNetCore.Annotations;

namespace AppQuiz.Api.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(IMediator mediator, ILogger<QuestionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation("Get all questions")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(IEnumerable<Question>))]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllQuestionQuery());
            //catch if failure
            return Ok(response);
        }

        [HttpGet]
        [Route("{questionId:guid}")]
        [SwaggerOperation("Get question by ID.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Question))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Question was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Get([FromQuery] GetQuestionByIdQuery query)
        {
            var response = await _mediator.Send(query);
            if (response == null)
            {
                return NotFound($"Question with ID '{query.QuestionId}' was not found.");
            }

            //catch if failure
            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation("Create question")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Post([FromBody] CreateQuestionCommand createQuestionCommand)
        {
            var response = await _mediator.Send(createQuestionCommand);
            //catch if failure
            return Ok(response);
        }

        [HttpPut]
        [Route("{questionId:guid}")]
        [SwaggerOperation("Update question")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Question was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Put([FromRoute] Guid questionId, [FromBody] UpdateQuestionCommand updateQuestionCommand)
        {
            updateQuestionCommand.SetId(questionId);
            var response = await _mediator.Send(updateQuestionCommand);
            //catch if failure
            return Ok(response);
        }

        [HttpDelete]
        [Route("{questionId:guid}")]
        [SwaggerOperation("Delete question")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Success.", typeof(Guid))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Question was not found.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error.")]
        public async Task<IActionResult> Delete([FromRoute] Guid questionId)
        {
            var response = await _mediator.Send(new DeleteQuestionCommand(questionId));
            //catch if failure
            return Ok(response);
        }
    }
}
