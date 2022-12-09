namespace Web.Areas.Admin.ViewModels.News
{
    public class NewsCreateVM
    {
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public IFormFile MainPhoto { get; set; }
        
    }
}
