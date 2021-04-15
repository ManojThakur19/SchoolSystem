using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebESchoolData.Repository;
using WebESchoolData.Services;

namespace WebESchoolData.DataContext
{  
    public static class ConfigureContext
    {
        public static void AddConfigureContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly("WebESchool")));
            services.AddDbContext<WebSchoolDataContext>(options => options.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly("WebESchool")));
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISchoolService, SchoolService>();
        }
    }
}
