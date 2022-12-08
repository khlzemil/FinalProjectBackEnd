using Core.Entities;

namespace Web.ViewModels
{
    public class HomeIndexVM
    {
        public AboutUs AboutUs { get; set; }
        public CoverVideo CoverVideo{ get; set; }
        public List<OurVision> OurVisions { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<HomeMainSlider> HomeMainSliders{ get; set; }
        public List<Statistic> Statistics{ get; set; }
        public WhyChoose WhyChoose { get; set; }
    }
}
