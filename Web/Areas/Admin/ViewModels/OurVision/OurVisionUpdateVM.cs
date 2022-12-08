namespace Web.Areas.Admin.ViewModels.OurVision
{
    public class OurVisionUpdateVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Icon { get; set; }
        public string? IconName { get; set; }
    }
}
