using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AboutUs : BaseEntity
    {
        public string? Title { get; set; }
        public string? Slogan { get; set; }
        public string? Description { get; set; }
        public string? IconName { get; set; }
        public ICollection<AboutUsPhoto>? AboutUsPhotos { get; set; }
    }
}
