using Core.Entities;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.QuestionTopic;

namespace Web.Areas.Admin.Services.Concrete
{
    public class QuestionTopicService : IQuestionTopicService
    {
        private readonly IQuestionTopicRepository _questionTopicRepository;
        private readonly ModelStateDictionary _modelState;
        public QuestionTopicService(IQuestionTopicRepository questionTopicRepository, IActionContextAccessor actionContextAccessor,IWebHostEnvironment webHostEnvironment)
        {
            _questionTopicRepository = questionTopicRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<bool> CreateAsync(QuestionTopicCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _questionTopicRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda kontent mövcuddur");
                return false;
            }

            var questionTopic = new QuestionTopic
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
            };

            await _questionTopicRepository.CreateAsync(questionTopic);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var questionTopic = await _questionTopicRepository.GetAsync(id);
            if (questionTopic != null)
            {
                await _questionTopicRepository.DeleteAsync(questionTopic);
                return true;
            }

            return false;
        }

        public async Task<QuestionTopicIndexVM> GetAllAsync()
        {
            var model = new QuestionTopicIndexVM
            {
                QuestionTopics = await _questionTopicRepository.GetAllAsync()
            };

            return model;
        }

        public async Task<QuestionTopicUpdateVM> GetUpdateModelAsync(int id)
        {
            var questionTopic = await _questionTopicRepository.GetAsync(id);

            if (questionTopic == null) return null;

            var model = new QuestionTopicUpdateVM
            {
                Id = questionTopic.Id,
                Title = questionTopic.Title,
                Description = questionTopic.Description,
            };

            return model;
        }

        public async Task<bool> UpdateAsync(QuestionTopicUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _questionTopicRepository.AnyAsync(s => s.Title.Trim().ToLower() == model.Title.Trim().ToLower() && model.Id != s.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "Bu adda Kontent mövcuddur");
                return false;
            }

            var questionTopic = await _questionTopicRepository.GetAsync(model.Id);

            if (questionTopic != null)
            {
                questionTopic.Title = model.Title;
                questionTopic.Description = model.Description;
                questionTopic.ModifiedAt = DateTime.Now;
                await _questionTopicRepository.UpdateAsync(questionTopic);
            }
            return true;
        }


    }
}
