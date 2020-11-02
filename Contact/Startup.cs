using System;
using System.Collections.Generic;
using System.Reflection;
using Contact.Commands;
using Contact.Domain;
using Contact.Middleware;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Platform.Domain;
using Platform.Messaging;
using Platform.Messaging.EventStore;
using Platform.Messaging.EventStore.Configuration;
using Platform.Messaging.EventStore.Factories;

namespace Contact
{
    class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }
        private static readonly string AssemblyName = "Contact";
        private const string Version = "v1";
        private static string GetTitle(string environmentName) => $"{AssemblyName} {Version} - ({environmentName})";

        public Startup(IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
                });
            services.AddRouting();


            services.AddMediatR(typeof(CreateContactCommand).GetTypeInfo().Assembly);


            services
                .AddSwaggerGen(options => options.SwaggerDoc(Version, new OpenApiInfo { Title = GetTitle(Environment.EnvironmentName), Version = Version }))
                .AddSwaggerGenNewtonsoftSupport();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                }
            };
            var eventStoreConfigKey = "EventStore";

            var eventStoreConfiguration = new EventStoreConfiguration();
            Configuration.GetSection(eventStoreConfigKey).Bind(eventStoreConfiguration);
            services.AddSingleton<IEventStoreConfiguration>(eventStoreConfiguration);

            // EventStore Registrations
            services.AddSingleton<IStreamNameProvider, StreamNameProvider>();
            services.AddSingleton<IEventStoreConnectionFactory, EventStoreConnectionFactory>();
            services.AddSingleton<IEventPersistenceClient, EventStoreClient>();


            // Register the AggregateWriter as both IAggregateReader and IAggregateWriter
            services.AddSingleton<ContactWriter>();
            services.AddSingleton<IAggregateWriter<Domain.Contact>>(i => i.GetService<ContactWriter>());
            services.AddSingleton<IAggregateReader<Domain.Contact>>(i => i.GetService<ContactWriter>());
        }

        public void Configure(IApplicationBuilder app,
            ILogger<Startup> logger)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                logger.LogError(e.ExceptionObject as Exception, "Unhandled exception");

            app.UseMiddleware<ExceptionToHttpStatusMiddleware>();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AssemblyName} v1 - ({Environment.EnvironmentName})");
            });

            logger.LogInformation("Service started!");
        }
    }
}
