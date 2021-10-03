using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.IO;
using TaskApp.Models;
using Xunit;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]

namespace TaskApp.IntegrationTest;

internal class TaskAppApplication : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            //services.AddValidatorsFromAssembly(Assembly.LoadFrom("TaskApp"));
            //services.AddMediatR(Assembly.LoadFrom("TaskApp"));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.Configure<MongoDatabaseSettings>(configuration.GetSection(nameof(MongoDatabaseSettings)));
            services.AddSingleton<IMongoDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);

            // services.AddScoped<ITaskItemRepositoty, TaskItemRepositoty>();
        });


        return base.CreateHost(builder);
    }


}

