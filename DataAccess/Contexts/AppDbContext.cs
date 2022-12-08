using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<AboutUsPhoto> AboutUsPhotos { get; set; }
        public DbSet<OurVision> OurVisions{ get; set; }
        public DbSet<HomeMainSlider> HomeMainSliders{ get; set; }
        public DbSet<CoverVideo> CoverVideos{ get; set; }
        public DbSet<WhyChoose> WhyChooses{ get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Statistic>  Statistics { get; set; }
        public DbSet<Pricing> Pricings{ get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionTopic> QuestionTopics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments{ get; set; }
    }
}
