
using ValidatorCPF.Application;
using ValidatorCPF.Application.IApplication;

namespace ValidatorCPF.Ioc
{
    public class InjectionModule : IInjection
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IDocument, DocumentApplication>();
        }
    }
}
