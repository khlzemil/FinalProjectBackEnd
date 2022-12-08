using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IFAQService
    {
        Task<FAQIndexVM> GetAllAsync();

        Task<FAQLoadQuestionIndexVM> LoadQuestionById(int id);
    }
}
