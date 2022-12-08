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
    public class OurVisionRepository : Repository<OurVision>, IOurVisionRepository
    {
        private readonly AppDbContext _context;

        public OurVisionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
