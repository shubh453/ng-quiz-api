using CheetahApi.Exceptions;
using CheetahApi.Extensions;
using CheetahApi.Infrastructure;
using CheetahApi.Model;
using CheetahApi.Utilities;

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
            var unfinishedQuestionnaires = await _questionnaireRepository.Get(q => q.Status == QuestionnaireStatus.NotTaken);
            
            if(unfinishedQuestionnaires is not null && unfinishedQuestionnaires.Any())
            {
                return unfinishedQuestionnaires.Last().ToDto();
            }

            var quiz = (await _quizRepository.Get(q => q.Category == category)).ToList();

            var randomQuizzes = 
                quiz.Count > 0 
                ? Randomizer.Randomize(quiz.Select(q => q.ToDto()), length: quiz.Count, take: count)
                : Array.Empty<QuizDto>();

            var result = await _questionnaireRepository.Save(new Questionnaire(category, randomQuizzes));
            return result.ToDto();
        }

        public async Task<Questionnaire> GetQuestionnaire(int id)
        {
            var questionnaire = await _questionnaireRepository.GetById(id);

            if (questionnaire == null)
                throw new NotFoundException<Questionnaire>(id);

            return questionnaire;
        }

        public async Task<QuestionnaireDto> SaveQuestionnaireResult(int questionnaireId, TestResult result)
        {
            var questionnaire = await _questionnaireRepository.GetById(questionnaireId);
            if (questionnaire == null)
                throw new NotFoundException<Questionnaire>(questionnaireId);

            try
            {
                var testResult = questionnaire.UpdateCurrentResult(result);
                await _questionnaireRepository.Update(questionnaireId, questionnaire);
                return new QuestionnaireDto(questionnaireId, questionnaire.Category, questionnaire.Quizzes, testResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return new QuestionnaireDto();
        }

        public async Task<bool> UpdateStatus(int id, QuestionnaireStatus questionnaireStatus)
        {
            var questionnaire = await _questionnaireRepository.GetById(id);
            if (questionnaire == null)
                throw new NotFoundException<Questionnaire>(id);

            try
            {
                switch (questionnaireStatus)
                {
                    case QuestionnaireStatus.Completed:
                        questionnaire.MarkCompleted();
                        break;
                    case QuestionnaireStatus.Retaken:
                        questionnaire.MarkRetaken();
                        break;
                    case QuestionnaireStatus.Started:
                        questionnaire.MarkStarted();
                        break;
                    case QuestionnaireStatus.NotTaken:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(questionnaireStatus), questionnaireStatus, null);
                }

                return await _questionnaireRepository.Update(id, questionnaire);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return false;
        }

    }
}
