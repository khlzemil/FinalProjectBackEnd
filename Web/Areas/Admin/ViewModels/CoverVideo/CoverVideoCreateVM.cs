namespace Web.Areas.Admin.ViewModels.CoverVideo
{
    public class CoverVideoCreateVM
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string? CoverImageName { get; set; }
        public IFormFile CoverPhoto { get; set; }
    }
}
