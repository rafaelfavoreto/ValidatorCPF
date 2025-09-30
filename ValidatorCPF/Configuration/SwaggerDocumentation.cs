




using Microsoft.OpenApi.Models;

namespace ValidatorCPF.Configuration
{
    public static class SwaggerDocumentation
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Validador de CPF/CNPJ",
                    Version = "v1",
                    Description = "API para validação e formatação de documentos brasileiros"
                });


            });
        }
    }
}
