namespace REST_API.Models;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string MenuCollectionName { get; set; } = null!;
}