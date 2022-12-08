using Core.Entities;
using Core.Utilities.Abstract;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.ViewModels.Department;

namespace Web.Areas.Admin.Services.Concrete
{
    public class DepartmentService : IDepartmentService
    {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IActionContextAccessor _actionContextAccessor;
            private readonly IFileService _fileService;
            private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly ModelStateDictionary _modelState;
            public DepartmentService(IDepartmentRepository departmentRepository,
                                    IActionContextAccessor actionContextAccessor,
                                    IFileService fileService,
                                    IWebHostEnvironment webHostEnvironment)
            {
                _departmentRepository = departmentRepository;
                _actionContextAccessor = actionContextAccessor;
                _fileService = fileService;
                _webHostEnvironment = webHostEnvironment;
                _modelState = actionContextAccessor.ActionContext.ModelState;
            }

            public async Task<bool> CreateAsync(DepartmentCreateVM model)
            {
                if (!_modelState.IsValid) return false;

                var isExist = await _departmentRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower());
                if (isExist)
                {
                    _modelState.AddModelError("Title", "Bu adda kontent mövcuddur");
                    return false;
                }

                bool hasError = false;

                if (!_fileService.IsImage(model.Icon))
                {
                    _modelState.AddModelError("Icon", $"{model.Icon.FileName} yuklediyiniz icon sekil formatinda olmalidir");
                    hasError = true;
                }

                if (hasError) { return false; }



                var department = new Department
                {
                    Title = model.Title,
                    Description = model.Description,
                    IconName = await _fileService.UploadAsync(model.Icon),
                    CreatedAt = DateTime.Now,
                };

                await _departmentRepository.CreateAsync(department);

                return true;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var department = await _departmentRepository.GetAsync(id);
                if (department != null)
                {
                    await _departmentRepository.DeleteAsync(department);
                    return true;
                }

                return false;
            }

            public async Task<DepartmentIndexVM> GetAllAsync()
            {
                var model = new DepartmentIndexVM
                {
                    Departments = await _departmentRepository.GetAllAsync()
                };
                return model;
            }
            public async Task<DepartmentUpdateVM> GetUpdateModelAsync(int id)
            {
                var department = await _departmentRepository.GetAsync(id);

                if (department == null) return null;

                var model = new DepartmentUpdateVM
                {
                    Id = department.Id,
                    Description = department.Description,
                    Title = department.Title,
                    IconName = department.IconName,
                };

                return model;
            }
            public async Task<bool> UpdateAsync(DepartmentUpdateVM model)
            {
                if (!_modelState.IsValid) return false;


                var isExist = await _departmentRepository.AnyAsync(c => c.Title.Trim().ToLower() == model.Title.Trim().ToLower() && c.Id != model.Id);
                if (isExist)
                {
                    _modelState.AddModelError("Title", "Bu adda kontent mövcuddur");
                    return false;
                }

                var ourVision = await _departmentRepository.GetAsync(model.Id);

                bool hasError = false;


                if (model.Icon != null)
                {
                    if (!_fileService.IsImage(model.Icon))
                    {
                        _modelState.AddModelError("Icon", $"{model.Icon.FileName} yuklediyiniz icon sekil formatinda olmalidir");
                        hasError = true;
                    }
                }

                if (hasError) { return false; }


                if (ourVision != null)
                {
                    ourVision.Title = model.Title;
                    ourVision.ModifiedAt = DateTime.Now;
                    ourVision.IconName = model.Icon != null ? await _fileService.UploadAsync(model.Icon) : ourVision.IconName;
                    ourVision.Description = model.Description;
                    await _departmentRepository.UpdateAsync(ourVision);

                }
                return true;
            }
        
    }
}
