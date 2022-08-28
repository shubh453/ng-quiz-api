using CheetahApi.Exceptions;
using CheetahApi.Extensions;
using CheetahApi.Model;
using CheetahApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CheetahApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnairesController : ControllerBase
    {
        private readonly IQuestionnaireService _questionnaireService;
        private readonly ILogger<QuestionnairesController> _logger;

        public QuestionnairesController(IQuestionnaireService questionnaireService, ILogger<QuestionnairesController> logger)
        {
            _questionnaireService = questionnaireService ?? throw new ArgumentNullException(nameof(questionnaireService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionnaireDto>>> Get(string category, int questioncount)
        {
            if (string.IsNullOrWhiteSpace(category) || questioncount <= 0)
            {
                return BadRequest();
            }

            try
            {
                var questionnaire = await _questionnaireService.GetQuestionnaire(category, questioncount);
                return Ok(new List<QuestionnaireDto> { questionnaire });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Questionnaire>> Get(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            try
            {
                var questionnaire = await _questionnaireService.GetQuestionnaire(id);
                return Ok(questionnaire);
            }
            catch (NotFoundException<Questionnaire> ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPatch("updateanswer/{id:int}")]
        public async Task<ActionResult<QuestionnaireDto>> updateanswer(int id, [FromBody] QuestionnaireDto questionnaire)
        {
            if (!ModelState.IsValid || id < 0)
            {
                return BadRequest();
            }
             
            try
            {
                var result = await _questionnaireService.SaveQuestionnaireResult(id, questionnaire.TestResult.ToResult());
                await _questionnaireService.UpdateStatus(id, QuestionnaireStatus.Completed);
                return Ok(result);
            }
            catch (NotFoundException<Questionnaire> ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch("updatestatus/{id:int}")]
        public async Task<ActionResult<QuestionnaireDto>> updatestatus(int id, [FromBody] QuestionnaireStatus status)
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
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
