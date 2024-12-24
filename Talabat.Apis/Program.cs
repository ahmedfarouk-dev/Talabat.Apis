
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Interfaces;
using Talabat.Repositories;
using Talabat.Repositories.Data;
using Talabat.Repositories.DataSeeding;

namespace Talabat.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreDbContext>(option =>

                           option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddScoped(typeof(IRepositories<>), typeof(GenericRepository<>));

            var app = builder.Build();
            #endregion

            #region Scope
            var Scope = app.Services.CreateScope();
            var Service = Scope.ServiceProvider;
            var _DbContext = Service.GetRequiredService<StoreDbContext>();

            try
            {
                await _DbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_DbContext);
            }
            catch (Exception Ex)
            {

                Console.WriteLine(Ex);
            }
            #endregion

            #region Configure the HTTP request pipeline.
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
