using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetPaginateAsync(IQueryable doctors,int page, int take)
        {
            return await _context.Doctors.OrderByDescending(b => b.Id).Skip((page - 1) * take).Take(take).ToListAsync();
        }

        public async Task<int> GetPageCountAsync(int take)
        {

            var blogsCount = await _context.Doctors.CountAsync();

            return (int)Math.Ceiling((decimal)blogsCount / take);

        }

        public async Task<List<Doctor>> FilterDoctors(string fullName, int page, int take)
        {
            var doctors =  FilterByTitle(fullName);
            return  await GetPaginateAsync(doctors, page, take);
        }

        public IQueryable<Doctor> FilterByTitle(string fullName)
        {
            return  _context.Doctors.Where(d => !string.IsNullOrEmpty(fullName) ? d.Name.Contains(fullName) : true);
        }

        
    }
}
