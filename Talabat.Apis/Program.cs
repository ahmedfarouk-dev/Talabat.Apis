using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Apis.Extension;
using Talabat.Apis.Helpers;
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
            builder.Services.AddAutoMapper(p => p.AddProfile(new MappingProfiles(builder.Configuration)));

            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var Connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(Connection);
            });


            builder.Services.AddApplicationServices();
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
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
