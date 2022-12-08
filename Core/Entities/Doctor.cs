using Core.Constants;
using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Profession Profession { get; set; }

        public string Bio { get; set; }
        public string Summary { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string PhotoName { get; set; }

        public DateTime MondayToFridayStartDate { get; set; } = DateTime.Parse("8:00");
        public DateTime MondayToFridayEndDate { get; set; } = DateTime.Parse("15:00");
        public DateTime SaturdayStart { get; set; } = DateTime.Parse("13:00");
        public DateTime SaturdayEnd { get; set; } = DateTime.Parse("17:00");

        public bool IsWorkingSunday { get; set; }

        public string? Skills { get; set; }

        public bool IsShowHomePage { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Linkedin { get; set; }

    }
}
