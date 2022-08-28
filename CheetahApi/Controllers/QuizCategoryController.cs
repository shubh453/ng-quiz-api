using CheetahApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CheetahApi.Controllers
{
    [Route("api/quizcategories")]
    [ApiController]
    public class QuizCategoryController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<QuizCategory>> Get()
        {
            return Ok(new List<QuizCategory>
            {
                new QuizCategory { Id = 1, Name = "rootwords", Description = "Find Root Word", Label = "Root Words"},
                new QuizCategory { Id = 1, Name = "antonyms", Description = "Find Opposite", Label = "Antonyms"},
                new QuizCategory { Id = 2, Name = "synonyms", Description = "Find Same", Label = "Synonyms"}
            });
        }
    }
}
