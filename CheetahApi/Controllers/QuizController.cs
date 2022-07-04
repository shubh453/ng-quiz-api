using CheetahApi.Extensions;
using CheetahApi.Model;
using CheetahApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace CheetahApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService ?? throw new ArgumentNullException(nameof(quizService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> Get()
        {
            var list = await _quizService.GetAllQuestions();
            return Ok(list.Select(q => q.ToDto()).ToList());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDto>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Id is invalid: {id}");
            }

            var question = await _quizService.GetQuestionById(id);

            return Ok(question.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<QuizDto>> Post([FromBody] IEnumerable<QuizDto> values)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var quizzes = values.ToArray();

            return quizzes.Length switch
            {
                0 => Ok(Enumerable.Empty<QuizDto>()),
                > 1 => Ok((await _quizService.SaveQuestions(quizzes.Select(v => v.ToModel()))).Select(d => d.ToDto())),
                _ => Ok((await _quizService.SaveQuestion(quizzes[0].ToModel())).ToDto())
            };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] QuizDto value)
        {
            if (!ModelState.IsValid || id <= 0)
            {
                return BadRequest();
            }

            var result = await _quizService.UpdateQuestion(id, value.ToModel());

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            return Ok(await _quizService.DeleteQuestionById(id));
        }

        [HttpDelete]
        public async Task<ActionResult<int>> Delete()
        {
            return Ok(await _quizService.DeleteAllQuestions());
        }
    }
}
