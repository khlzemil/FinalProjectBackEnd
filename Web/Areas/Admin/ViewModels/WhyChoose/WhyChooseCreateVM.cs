namespace Web.Areas.Admin.ViewModels.WhyChoose
{
    public class WhyChooseCreateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string? PhotoName { get; set; }
        public IFormFile Photo { get; set; }
    }
}
