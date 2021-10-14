using Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application;
using PaymentGateway.ExternalService;
using PaymentGateway.WebApi.Swagger;
using MediatR;
using PaymentGateway.Application.Queries;

namespace PaymentGateway.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(o => o.EnableEndpointRouting = false);

            //services.AddSingleton<IEventSender, EventSender>();
            //services.AddMediatR(typeof(EnrollAgentComandHandler).Assembly);
            var firstAssembly = typeof(ListOfAccounts).Assembly; // handlere c1..c3
            //var firstAssembly = typeof(Program).Assembly; // handler generic
            var secondAssembly = typeof(AllEventsHandler).Assembly; // catch all
            //var trdasembly = System.Reflection.Assembly.LoadFrom("c:/a.dll");
            services.AddMediatR(firstAssembly, secondAssembly); // get all IRequestHandler and INotificationHandler classes

            services.RegisterBusinessServices(Configuration);
            services.AddSwagger(Configuration["Identity:Authority"]);

            // NEVER USE
            //services.BuildServiceProvider(); => serviceProvider...lista de "matrite"
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseCors(cors =>
            {
                cors
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });

#pragma warning disable MVC1005 // Cannot use UseMvc with Endpoint Routing.
            app.UseMvc();
#pragma warning restore MVC1005 // Cannot use UseMvc with Endpoint Routing.
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment gateway Api");
                //c.OAuthClientId("CharismaFinancialServices");
                //c.OAuthScopeSeparator(" ");
                c.EnableValidator(null);
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}