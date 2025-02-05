using Serilog;
using Users.Api.Helpers;
using Users.Application.UserService;
using Users.Core.Interfaces;
using Users.Infrastructure.Repository;

namespace Users.Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("WebApiDatabase");
            services.AddScoped<IUserRepository>(provider => new UserRepository(connectionString));
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddLogging(builder => builder.AddSerilog());
            services.AddSingleton(Log.Logger);
            return services;
        }
    }
}
