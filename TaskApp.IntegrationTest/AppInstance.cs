using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;
using TaskApp.Behaviors;

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




            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

        }


        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

    }
}
