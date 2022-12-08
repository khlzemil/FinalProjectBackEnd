namespace Web.Areas.Admin.ViewModels.HomeMainSlider
{
    public class HomeMainSliderCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LearnMore { get; set; }
        public string UrlAdress { get; set; }
        public int Order { get; set; }
        public string? PhotoName { get; set; }
        public IFormFile Photo { get; set; }
    }
}
