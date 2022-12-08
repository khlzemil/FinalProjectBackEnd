using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<Question>> GetWithTopicAsync();
        Task<List<Question>> GetQuestionsByTopicIdAsync(int id);
    }
}
