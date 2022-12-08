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
    public class QuestionTopicRepository : Repository<QuestionTopic>, IQuestionTopicRepository
    {
        private readonly AppDbContext _context;

        public QuestionTopicRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        

    }
}
