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
    public class CoverVideoRepository : Repository<CoverVideo>, ICoverVideoRepository
    {
        private readonly AppDbContext _context;

        public CoverVideoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CoverVideo> GetAsync()
        {
            return await _context.CoverVideos.FirstOrDefaultAsync();
        }
    }
}
