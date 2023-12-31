using Context.Data;
using Application.Interfaces;
using Context.Models;
using Application.PhotoCloudThing;
using Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.SMSSetUp;

namespace I_STORE
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ISneakerService, SneakerService>();
            builder.Services.AddScoped<IUpperBodyService, UpperBodyService>();
            builder.Services.AddScoped<ILowerBodyService, LowerBodyService>();
            builder.Services.AddScoped<IUserDashBoardService, UserDashBoardService>();
            builder.Services.AddScoped<IAdminDashBoardService, AdminDashBoardService>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<Microsoft.AspNet.Identity.IIdentityMessageService, SMSService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.Configure<CloudinarySetup>(builder.Configuration.GetSection("CloudinarySetup"));
            builder.Services.Configure<SMSSetUp>(builder.Configuration.GetSection("SMSSetUp"));
            
            var configuration = builder.Configuration;
            //builder.Services.AddAuthentication().AddGoogle(
            //    options =>
            //    {
            //        options.ClientId = configuration["Authentication:Google:ClientId"];
            //        options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            //    });



            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddMemoryCache();

            builder.Services.AddSession();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();


            var app = builder.Build();
            if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
                //await SeedData.SeedUsersAndRolesAsync(app);
                //SeedData.SneakerSeedData(app);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}