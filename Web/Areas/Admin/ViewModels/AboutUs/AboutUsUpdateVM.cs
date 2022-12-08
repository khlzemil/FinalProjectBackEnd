using Core.Entities;

namespace Web.Areas.Admin.ViewModels.AboutUs
{
    public class AboutUsUpdateVM
    {
        public AboutUsUpdateVM()
        {
            Photos = new List<IFormFile>();
        }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Slogan { get; set; }
        public string? Description { get; set; }
        public IFormFile? Icon { get; set; }
        public string? IconName { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public ICollection<AboutUsPhoto>? AboutUsPhotos { get; set; }

    }
}
