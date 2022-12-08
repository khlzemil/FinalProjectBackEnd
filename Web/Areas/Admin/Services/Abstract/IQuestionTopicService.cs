using Web.Areas.Admin.ViewModels.QuestionTopic;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IQuestionTopicService
    {
        Task<QuestionTopicIndexVM> GetAllAsync();
        Task<bool> CreateAsync(QuestionTopicCreateVM model);
        Task<QuestionTopicUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(QuestionTopicUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
