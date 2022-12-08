using Core.Entities;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Admin.Services.Abstract;
using Web.Areas.Admin.Services.Concrete;
using Web.Services.Abstract;
using Web.Services.Concrete;

using AdminAbstractService = Web.Areas.Admin.Services.Abstract;
using AbstractService = Web.Services.Abstract;
using AdminConcreteService = Web.Areas.Admin.Services.Concrete;
using ConcreteService = Web.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>(); //



var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString, x => x.MigrationsAssembly("DataAccess")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>();

#region Repositories
builder.Services.AddScoped<IAboutUsRepository, AboutUsRepository>();
builder.Services.AddScoped<IAboutUsPhotoRepository, AboutUsPhotoRepository>();
builder.Services.AddScoped<IOurVisionRepository, OurVisionRepository>();
builder.Services.AddScoped<IHomeMainSliderRepository, HomeMainSliderRepository>();
builder.Services.AddScoped<ICoverVideoRepository, CoverVideoRepository>();
builder.Services.AddScoped<IWhyChooseRepository, WhyChooseRepository>();
builder.Services.AddScoped<IStatisticRepository, StatisticRepository>();
builder.Services.AddScoped<IPricingRepository, PricingRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IQuestionTopicRepository, QuestionTopicRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();




#endregion

#region Services

builder.Services.AddScoped<IAboutUsService, AboutUsService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IPagesService, PagesService>();
builder.Services.AddScoped<IOurVisionService, OurVisionService>();
builder.Services.AddScoped<IHomeMainSliderService, HomeMainSliderService>();
builder.Services.AddScoped<ICoverVideoService, CoverVideoService>();
builder.Services.AddScoped<IWhyChooseService, WhyChooseService>();
builder.Services.AddScoped<IStatisticService, StatisticService>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IQuestionTopicService, QuestionTopicService>();
builder.Services.AddScoped<AdminAbstractService.IProductCategoryService, AdminConcreteService.ProductCategoryService>();
builder.Services.AddScoped<AdminAbstractService.IProductService, AdminConcreteService.ProductService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IFAQService, FAQService>();
builder.Services.AddScoped<AdminAbstractService.IDepartmentService, AdminConcreteService.DepartmentService>();
builder.Services.AddScoped<AbstractService.IDepartmentService, ConcreteService.DepartmentService>();
builder.Services.AddScoped<AdminAbstractService.IDoctorService, AdminConcreteService.DoctorService>();
builder.Services.AddScoped<AbstractService.IDoctorService, ConcreteService.DoctorService>();
builder.Services.AddScoped<AdminAbstractService.IAccountService, AdminConcreteService.AccountService>();
builder.Services.AddScoped<AbstractService.IAccountService, ConcreteService.AccountService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddScoped<IShopService, ShopService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedAsync(userManager, roleManager);
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=account}/{action=login}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
