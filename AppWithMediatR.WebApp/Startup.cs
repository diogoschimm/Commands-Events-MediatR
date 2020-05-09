using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppWithMediatR.ApplicationLayer.CommandHandlers;
using AppWithMediatR.ApplicationLayer.Commands;
using AppWithMediatR.ApplicationLayer.EventHandlers;
using AppWithMediatR.ApplicationLayer.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AppWithMediatR.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMediatR(typeof(Startup));

            services.AddTransient<IRequestHandler<RegistrarVendaCommand, bool>, RegistrarVendaCommandHandler>();
            services.AddTransient<IRequestHandler<RegistrarPagamentoCommand, bool>, RegistrarPagamentoCommandHandler>();

            services.AddTransient<INotificationHandler<VendaRealizadaComSucessoEvent>, RegistrarVendaEventHandler>();
            services.AddTransient<INotificationHandler<VendaRealizadaComErroEvent>, RegistrarVendaEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
