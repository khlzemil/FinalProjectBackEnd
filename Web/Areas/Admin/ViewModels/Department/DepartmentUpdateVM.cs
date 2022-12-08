﻿namespace Web.Areas.Admin.ViewModels.Department
{
    public class DepartmentUpdateVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Icon { get; set; }
        public string? IconName { get; set; }
    }
}
