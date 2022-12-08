namespace Web.Areas.Admin.ViewModels.Statistic
{
    public class StatisticCreateVM
    {
        public StatisticCreateVM()
        {
            Title = " - ";
        }
        public int Count { get; set; }
        public string? Title { get; set; }
    }
}
