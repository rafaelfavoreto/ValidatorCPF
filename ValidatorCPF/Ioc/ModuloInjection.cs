namespace ValidatorCPF.Ioc
{
    public static class ModuloInjection
    {
        public static IServiceCollection AddCustomInjection(this IServiceCollection services)
        {
            new InjectionModule().RegisterServices(services);
            return services;
        }

        public static void RegisterInternalServices(this IServiceCollection services)
        {
            var modules = AppDomain.CurrentDomain.GetAssemblies()
                 .SelectMany(row => row.GetTypes())
                 .Where(row => typeof(IInjection).IsAssignableFrom(row) && row.Name != nameof(IInjection))
                 .ToList();
                  
            foreach (var item in modules)                    
            {                    
                var instance = Activator.CreateInstance(item) as IInjection;                   
                instance?.RegisterServices(services);                   
            }
        }

    }
}

