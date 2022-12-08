using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.ViewModels.Question
{
    public class QuestionCreateVM
    {
        public string Problem { get; set; }
        public string Solve { get; set; }

        public int QuestionTopicId { get; set; }
        public List<SelectListItem>? QuestionTopics { get; set; }
    }
}
