
namespace TaskApp.Models;
public class MongoDatabaseSettings : IMongoDatabaseSettings
{
    public string CollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}

public interface IMongoDatabaseSettings
{
    string CollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }

}
