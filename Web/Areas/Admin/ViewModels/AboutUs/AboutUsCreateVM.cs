namespace Web.Areas.Admin.ViewModels.AboutUs
{
    public class AboutUsCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slogan { get; set; }
        public string Description { get; set; }
        public IFormFile Icon{ get; set; }
        public List<IFormFile>? AboutUsPhotos { get; set; }
    }
}
