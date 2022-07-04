using CheetahApi.Exceptions;
using CheetahApi.Extensions;
using CheetahApi.Infrastructure;
using CheetahApi.Model;

namespace CheetahApi.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IQuestionnaireRepository _questionnaireRepository;
        private readonly ILogger<QuestionnaireService> _logger;

        public QuestionnaireService(IQuizRepository quizRepository, IQuestionnaireRepository questionnaireRepository,
            ILogger<QuestionnaireService> logger)
        {
            _quizRepository = quizRepository ?? throw new ArgumentNullException(nameof(quizRepository));
            _questionnaireRepository = questionnaireRepository ??
                                       throw new ArgumentNullException(nameof(questionnaireRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<QuestionnaireDto> GetQuestionnaire(string category, int count)
        {
            var quiz = (await _quizRepository.Get(q => q.Category == category)).ToList();
            var randomizedQuiz = new List<QuizDto>();
            for (var i = 0; i < count; i++)
            {
                randomizedQuiz.Add(quiz[Random.Shared.Next(quiz.Count - 1)].ToDto());
            }

            var result = await _questionnaireRepository.Save(new Questionnaire(randomizedQuiz));

            return result.ToDto();
        }

        public async Task<bool> SaveQuestionnaireResult(int questionnaireId, TestResult result)
        {
            var questionnaire = await _questionnaireRepository.GetById(questionnaireId);
            if (questionnaire == null)
                throw new NotFoundException<Questionnaire>(questionnaireId);

            try
            {
                questionnaire.UpdateCurrentResult(result);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return false;
        }

        public async Task<QuestionnaireStatus> UpdateStatus(int id, QuestionnaireStatus questionnaireStatus)
        {
            var questionnaire = await _questionnaireRepository.GetById(id);
            if (questionnaire == null)
                throw new NotFoundException<Questionnaire>(id);

            try
            {
                if (questionnaireStatus == QuestionnaireStatus.Completed)
                    return questionnaire.MarkCompleted();
                if (questionnaireStatus == QuestionnaireStatus.Retaken)
                    return questionnaire.MarkRetaken();
                if (questionnaireStatus == QuestionnaireStatus.Started)
                    return questionnaire.MarkStarted();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return QuestionnaireStatus.NotTaken;
        }

    }
}
