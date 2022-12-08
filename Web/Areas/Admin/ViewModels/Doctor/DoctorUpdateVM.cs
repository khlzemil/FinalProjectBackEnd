using Core.Constants;

namespace Web.Areas.Admin.ViewModels.Doctor
{
    public class DoctorUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Profession Profession { get; set; }

        public string Bio { get; set; }
        public string Summary { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string? PhotoName { get; set; }
        public IFormFile Photo { get; set; }

        public DateTime MondayToFridayStartDate { get; set; }
        public DateTime MondayToFridayEndDate { get; set; }
        public DateTime SaturdayStart { get; set; }
        public DateTime SaturdayEnd { get; set; }

        public bool IsWorkingSunday { get; set; }

        public string? Skills { get; set; }

        public bool IsShowHomePage { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Linkedin { get; set; }
    }
}
