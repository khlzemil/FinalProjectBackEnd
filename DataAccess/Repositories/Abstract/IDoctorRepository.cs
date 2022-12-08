using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Task<List<Doctor>> GetPaginateAsync(IQueryable doctors, int page, int take);
        Task<int> GetPageCountAsync(int take);
        Task<List<Doctor>> FilterDoctors(string fullName, int page, int take);
        public IQueryable<Doctor> FilterByTitle(string fullName);
    }
}
