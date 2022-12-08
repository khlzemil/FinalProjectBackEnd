using Core.Entities;

namespace Web.ViewModels
{
    public class DoctorIndexVM
    {
        public List<Doctor> Doctors { get; set; }

        public string Name{ get; set; }
        public int Page { get; set; } = 1;

        public int Take { get; set; } = 3;

        public int PageCount { get; set; }


    }
}
