using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Question : BaseEntity
    {
        public string Problem { get; set; }
        public string Solve { get; set; }

        public int QuestionTopicId { get; set; }
        public QuestionTopic? QuestionTopic { get; set; }
        
    }
}
