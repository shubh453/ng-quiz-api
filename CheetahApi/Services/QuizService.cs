using CheetahApi.Infrastructure;
using CheetahApi.Model;

namespace CheetahApi.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly ILogger<QuizService> _logger;

        public QuizService(IQuizRepository quizRepository, ILogger<QuizService> logger)
        {
            _quizRepository = quizRepository ?? throw new ArgumentNullException(nameof(quizRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Quiz>> GetAllQuestions()
        {
            return await _quizRepository.GetAll();
        }

        public async Task<Quiz> GetQuestionById(int id)
        {
            return await _quizRepository.GetById(id);
        }

        public async Task<Quiz> SaveQuestion(Quiz quiz)
        {
            return await _quizRepository.Save(quiz);
        }

        public async Task<IEnumerable<Quiz>> SaveQuestions(IEnumerable<Quiz> quizzes)
        {
            try
            {
                var quizArray = quizzes as Quiz[] ?? quizzes.ToArray();
                await _quizRepository.Save(quizArray);
                var allItems = await _quizRepository.GetAll();
                return allItems.Where(i => quizArray.Any(x => x.UniqueIdentifier.Equals(i.UniqueIdentifier))).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public async Task<bool> DeleteQuestionById(int id)
        {
            return await _quizRepository.DeleteById(id);
        }

        public async Task<bool> UpdateQuestion(int id, Quiz question)
        {
            return await _quizRepository.Update(id, question);
        }

        public async Task<int> DeleteAllQuestions()
        {
            return await _quizRepository.DeleteAll();
        }
    }
}
