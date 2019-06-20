using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FoodBot.Bots;
using FoodBot.Commands.CommandHandling;
using FoodBot.Repositories;
using FoodBot.Model;

namespace FoodBot {
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<ICredentialProvider, ConfigurationCredentialProvider>();
            services.AddSingleton<IBotFrameworkHttpAdapter, BotFrameworkHttpAdapter>();
            services.AddTransient<IBot, Bot>();
            services.AddSingleton(new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate}));
            services.AddSingleton<ICommandBroker, CommandBroker>();
            services.AddSingleton<ICommandFactory, CommandFactory>();
            services.AddSingleton<ICommandHistory, CommandHistory>();
            services.AddSingleton<IRepository<Order>, PersistedInMemoryRepository<Order>>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseMvc(route => {
                route.MapRoute("default", "{controller}/{action}", new { controller = "default", action = "index" });
            });
        }

    }
}
