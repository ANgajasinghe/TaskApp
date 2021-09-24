using MongoDB.Driver;
using System.Linq;
using TaskApp.Models;
using Xunit;

namespace TaskApp.IntegrationTest;

public class DbConnectionTest : IClassFixture<AppInstance>
{
    private readonly AppInstance _appInstance;
    private readonly IMongoDatabaseSettings _mongoDatabaseSettings;

    public DbConnectionTest(AppInstance appInstance)
    {
        _appInstance = appInstance;
        _mongoDatabaseSettings = appInstance.MongoDatabaseSettings;
    }

    [Fact]
    public void ShouldDatabaseHas_taskApp()
    {
        var client = new MongoClient(_mongoDatabaseSettings.ConnectionString);
        var dbList = client.ListDatabaseNames().ToList();
        Assert.NotEmpty(dbList);
        var hasTaskApp = dbList.Any(x => x == _mongoDatabaseSettings.DatabaseName);
        Assert.True(hasTaskApp);

    }
}
