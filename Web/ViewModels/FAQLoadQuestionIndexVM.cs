using Core.Entities;

namespace Web.ViewModels
{
    public class FAQLoadQuestionIndexVM
    {
        public QuestionTopic QuestionTopic { get; set; }
        public List<Question> Questions{ get; set; }
    }
}
