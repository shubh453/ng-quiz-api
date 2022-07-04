using CheetahApi.Exceptions;
using CheetahApi.Extensions;
using CheetahApi.Model;
using CheetahApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheetahApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQuestionnaireService _questionnaireService;
        private readonly ILogger<QuestionnaireController> _logger;

        public QuestionnaireController(IQuestionnaireService questionnaireService, ILogger<QuestionnaireController> logger)
        {
            _questionnaireService = questionnaireService ?? throw new ArgumentNullException(nameof(questionnaireService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{category}")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> Get(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest();
            }

            var questionnaire = await _questionnaireService.GetQuestionnaire(category);
            return Ok(questionnaire);
        }

        [HttpGet("{category}/{count:int}")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> Get(string category, int count)
        {
            if (string.IsNullOrWhiteSpace(category) || count <= 0)
            {
                return BadRequest();
            }

            var questionnaire = await _questionnaireService.GetQuestionnaire(category, count);
            return Ok(questionnaire);
        }

        [HttpPost("{id:int}")]
        public async Task<ActionResult<bool>> Post(int id, [FromBody] TestResultDto testResult)
        {
            if (!ModelState.IsValid || id < 0)
            {
                return BadRequest();
            }

            try
            {
                var result = await _questionnaireService.SaveQuestionnaireResult(id, testResult.ToResult());
                return Ok(result);
            }
            catch (NotFoundException<Questionnaire> ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPatch("updatestatus/{id:int}")]
        public async Task<ActionResult<QuestionnaireStatus>> Put(int id, [FromBody] QuestionnaireStatus status)
        {
            if (!ModelState.IsValid || id < 0)
            {
                return BadRequest();
            }

            try
            {
                var result = await _questionnaireService.UpdateStatus(id, status);
                return Ok(result);
            }
            catch (NotFoundException<Questionnaire> ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

    }
}
