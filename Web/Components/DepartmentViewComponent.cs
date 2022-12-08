using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;

namespace Web.Components
{
    public class DepartmentViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public DepartmentViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new DepartmentIndexVM
            {
                Departments = await _appDbContext.Departments.ToListAsync()
            };

            return View(model);
        }
    }
}
