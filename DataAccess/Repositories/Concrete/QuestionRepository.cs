using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetWithTopicAsync()
        {
            return await _context.Questions.Include(p => p.QuestionTopic).ToListAsync();
        }


        public async Task<List<Question>> GetQuestionsByTopicIdAsync(int id)
        {
            return await _context.Questions.Where(p => p.QuestionTopicId == id).ToListAsync();
        }
    }
}
