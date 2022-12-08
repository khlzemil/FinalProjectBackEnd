using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;

namespace Web.Components
{
    public class DoctorViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public DoctorViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new DoctorIndexVM
            {
                Doctors = await _appDbContext.Doctors.ToListAsync()
            };

            return View(model);
        }
    }
}
