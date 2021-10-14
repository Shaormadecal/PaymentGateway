using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace PaymentGateway.WebApi.Swagger
{
    public static class SwaggerExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="authority"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, string authority)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Payment gateway Api",
                        Version = "v1"
                    }
                );

                // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
                //c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.OAuth2,
                //    Flows = new OpenApiOAuthFlows
                //    {
                //        Implicit = new OpenApiOAuthFlow
                //        {
                //            AuthorizationUrl = new Uri(authority + "/connect/authorize", UriKind.Absolute),
                //            Scopes = new Dictionary<string, string>
                //            {
                //                {"LSNG.Api.read_only", "Access read operations"},
                //                {"charisma_data", "Charisma operations"}
                //            }
                //        }
                //    }
                //});

                // Assign scope requirements to operations based on AuthorizeAttribute
                //c.OperationFilter<SecurityRequirementsOperationFilter>();
                //c.SchemaFilter<DtoSchemaFilter>();
                //c.OperationFilter<PathParamsOperationFilter>();
                c.CustomSchemaIds(type => type.ToString());

                //var documentedAssemblies = new[]
                //{
                //    typeof(Api.Controllers.CallbackController).Assembly,
                //    typeof(PublishedLanguage.Commands.DownloadDocuments).Assembly
                //};

                //foreach (var assembly in documentedAssemblies)
                //{
                //    var filePath = Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml");
                //    if (File.Exists(filePath))
                //    {
                //        c.IncludeXmlComments(filePath);
                //    }
                //}
            });

            return services;
        }
    }
}
