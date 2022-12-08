using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;


        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<DepartmentIndexVM> GetAllAsync()
        {
            var model = new DepartmentIndexVM
            {
                Departments = await _departmentRepository.GetAllAsync()
            };
            return model;
        }
    }
}
