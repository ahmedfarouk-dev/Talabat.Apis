using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;
using Talabat.Apis.Extension;
using Talabat.Apis.Helpers;
using Talabat.Repositories.Data;
using Talabat.Repositories.DataSeeding;
using Talabat.Repositories.Identity;

namespace Talabat.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.Configure<Jwt>(builder.Configuration.GetSection("JwtSettings"));

            builder.Services.AddDbContext<StoreDbContext>(option =>

                           option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddDbContext<StoreIdentity>(option =>

                          option.UseSqlServer(builder.Configuration.GetConnectionString("Identity"))
           );



            builder.Services.AddAutoMapper(p => p.AddProfile(new MappingProfiles(builder.Configuration)));

            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var Connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(Connection);
            });


            builder.Services.AddApplicationServices();
            builder.Services.AddIdentutyAndJwtApplicationServices(builder.Configuration);

            var app = builder.Build();
            #endregion

            #region Scope
            var Scope = app.Services.CreateScope();
            var Service = Scope.ServiceProvider;
            var _DbContext = Service.GetRequiredService<StoreDbContext>();
            var _DbContextIdentity = Service.GetRequiredService<StoreIdentity>();

            try
            {
                await _DbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_DbContext);
                await _DbContextIdentity.Database.MigrateAsync();
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
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
