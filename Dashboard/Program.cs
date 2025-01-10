using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Repositories.Data;
using Talabat.Repositories.Identity;

namespace Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<StoreDbContext>(option =>

                      option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
       );

            builder.Services.AddDbContext<StoreIdentity>(option =>

                          option.UseSqlServer(builder.Configuration.GetConnectionString("Identity"))
           );
            builder.Services.AddIdentity<UserApplication, IdentityRole>()
             .AddEntityFrameworkStores<StoreIdentity>()
         .AddDefaultTokenProviders();
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

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
