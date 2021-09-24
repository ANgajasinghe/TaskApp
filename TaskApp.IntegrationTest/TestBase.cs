using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using TaskApp.Behaviors;
using TaskApp.Models;
using TaskApp.Persistence;
using Xunit;

namespace TaskApp.IntegrationTest
{
    public class TestBase : IAsyncLifetime
    {

        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;

        public ITaskItemRepositoty TaskItemRepositoty { get; private set; }

        public Task DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public async Task InitializeAsync()
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", true, true)
           .AddEnvironmentVariables();

            _configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                    w.EnvironmentName == "Development" &&
                    w.ApplicationName == "CleanArchitecture.WebUI"));

            services.AddLogging();

            services.Configure<MongoDatabaseSettings>(_configuration.GetSection(nameof(MongoDatabaseSettings)));
            services.AddSingleton<IMongoDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

            services.AddValidatorsFromAssembly(Assembly.LoadFrom("TaskApp"));
            services.AddMediatR(Assembly.LoadFrom("TaskApp"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<ITaskItemRepositoty, TaskItemRepositoty>();

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>() ?? throw new ArgumentException();


            InitRepositories();

        }


        private static void InitRepositories()
        {
            using var scope = _scopeFactory.CreateScope();

            // TaskItemRepositoty = scope.ServiceProvider.GetService<ITaskItemRepositoty>() ?? throw new ArgumentException();

        }
    }
}
