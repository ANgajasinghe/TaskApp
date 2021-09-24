using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using TaskApp.Behaviors;
using TaskApp.Models;
using TaskApp.Persistence;

namespace TaskApp.IntegrationTest
{
    public class AppInstance
    {
        private static IServiceScopeFactory _scopeFactory;

        public AppInstance()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

            var configurations = builder.Build();

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "TaskApp"));

            services.AddLogging();

            services.AddValidatorsFromAssembly(Assembly.LoadFrom("TaskApp"));
            services.AddMediatR(Assembly.LoadFrom("TaskApp"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.Configure<MongoDatabaseSettings>(configurations.GetSection(nameof(MongoDatabaseSettings)));
            services.AddSingleton<IMongoDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

            services.AddScoped<ITaskItemRepositoty, TaskItemRepositoty>();

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            ABC();


        }

        public void ABC()
        {
            using var scope = _scopeFactory.CreateScope();
            var d = scope.ServiceProvider.GetService<ITaskItemRepositoty>();
        }


        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

    }
}
