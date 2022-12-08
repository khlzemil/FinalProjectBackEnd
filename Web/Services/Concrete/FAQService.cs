using Core.Entities;
using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class FAQService : IFAQService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionTopicRepository _questionTopicRepository;


        public FAQService(IQuestionRepository questionRepository, IQuestionTopicRepository questionTopicRepository)
        {
            _questionRepository = questionRepository;
            _questionTopicRepository = questionTopicRepository;
        }
        public async Task<FAQIndexVM> GetAllAsync()
        {
            var model = new FAQIndexVM
            {
                Questions = await _questionRepository.GetAllAsync(),
                QuestionTopics = await _questionTopicRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<FAQLoadQuestionIndexVM> LoadQuestionById(int id)
        {
            var questionTopic = await _questionTopicRepository.GetAsync(id);

            var model = new FAQLoadQuestionIndexVM
            {
                QuestionTopic = questionTopic,

                Questions = questionTopic != null ? await _questionRepository.GetQuestionsByTopicIdAsync(questionTopic.Id) : new List<Question>()
            };

            return model;
        }
    }
}
