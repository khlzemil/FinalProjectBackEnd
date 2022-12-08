using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class StatisticRepository : Repository<Statistic>, IStatisticRepository
    {
        private readonly AppDbContext _context;

        public StatisticRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
