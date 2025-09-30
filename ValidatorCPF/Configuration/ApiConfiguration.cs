using ValidatorCPF.Ioc;

namespace ValidatorCPF.Configuration
{
    public static class ApiConfiguration
    {
        public static void AddApiConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterInternalServices();
            services.AddControllers();
            services.AddEndpointsApiExplorer();        
            services.AddSwaggerGen();
            AddCors(services);
            services.AddCustomInjection();
        }

        private static IServiceCollection AddCors(IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }
    }
}
