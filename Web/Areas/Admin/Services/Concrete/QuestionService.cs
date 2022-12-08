using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Question;

namespace Web.Areas.Admin.Services.Concrete
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionTopicRepository _questionTopicRepository;
        private readonly ModelStateDictionary _modelState;
        public QuestionService(IQuestionRepository questionRepository,IQuestionTopicRepository questionTopicRepository , IActionContextAccessor actionContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _questionRepository = questionRepository;
            _questionTopicRepository = questionTopicRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        

        public async Task<QuestionIndexVM> GetAllAsync()
        {
            var model = new QuestionIndexVM
            {
                Questions = await _questionRepository.GetWithTopicAsync()
            };

            return model;
        }

        public async Task<QuestionCreateVM> GetCreateModelAsync()
        {
            var questionTopic = await _questionTopicRepository.GetAllAsync();

            var model = new QuestionCreateVM
            {
                QuestionTopics = questionTopic.Select(q => new SelectListItem
                {
                    Text = q.Title,
                    Value = q.Id.ToString()
                }).ToList()
            };
            return model;
        }

        public async Task<bool> CreateAsync(QuestionCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var questionTopic = await _questionRepository.GetWithTopicAsync();

            if (model.QuestionTopicId == null) return false;


            var question = new Question
            {
                Problem = model.Problem,
                Solve = model.Solve,
                QuestionTopicId = model.QuestionTopicId,
                CreatedAt = DateTime.Now,
            };

            await _questionRepository.CreateAsync(question);

            return true;
        }

        public async Task<QuestionUpdateVM> GetUpdateModelAsync(int id)
        {
            var question = await _questionRepository.GetAsync(id);

            var questionTopic = await _questionTopicRepository.GetAllAsync();

            if (question == null) return null;

            var model = new QuestionUpdateVM
            {
                Id = question.Id,
                Solve = question.Solve,
                Problem = question.Problem,
                QuestionTopics = questionTopic.Select(q => new SelectListItem
                {
                    Text = q.Title,
                    Value = q.Id.ToString()
                }).ToList(),
                QuestionTopicId = question.QuestionTopicId
            };

            return model;
        }

        public async Task<bool> UpdateAsync(QuestionUpdateVM model)
        {
            if (!_modelState.IsValid) return false;


            var questionTopic = await _questionRepository.GetAsync(model.Id);

            if (questionTopic != null)
            {
                questionTopic.Problem = model.Problem;
                questionTopic.Solve = model.Solve;
                questionTopic.QuestionTopicId = model.QuestionTopicId;
                questionTopic.ModifiedAt = DateTime.Now;
                await _questionRepository.UpdateAsync(questionTopic);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var question = await _questionRepository.GetAsync(id);
            if (question != null)
            {
                await _questionRepository.DeleteAsync(question);
                return true;
            }

            return false;
        }
    }
}
