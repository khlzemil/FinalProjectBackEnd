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
    public class HomeMainSliderRepository : Repository<HomeMainSlider>, IHomeMainSliderRepository
    {
        private readonly AppDbContext _context;

        public HomeMainSliderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<HomeMainSlider>> GetAllOrderedAsync()
        {
            return await _context.HomeMainSliders.OrderBy(o => o.Order).ToListAsync();
        }
    }
}
