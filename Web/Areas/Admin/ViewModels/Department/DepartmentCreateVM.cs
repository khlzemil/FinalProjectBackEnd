namespace Web.Areas.Admin.ViewModels.Department
{
    public class DepartmentCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? IconName { get; set; }
        public IFormFile Icon{ get; set; }
    }
}
