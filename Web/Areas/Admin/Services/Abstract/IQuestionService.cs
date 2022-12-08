using Web.Areas.Admin.ViewModels.Question;

namespace Web.Areas.Admin.Services.Abstract
{
    public interface IQuestionService
    {
        Task<QuestionIndexVM> GetAllAsync();

        Task<QuestionCreateVM> GetCreateModelAsync();

        Task<bool> CreateAsync(QuestionCreateVM model);

        Task<QuestionUpdateVM> GetUpdateModelAsync(int id);

        Task<bool> UpdateAsync(QuestionUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
