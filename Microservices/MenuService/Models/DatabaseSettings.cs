namespace MenuService.Models;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string MenuItemsCollectionName { get; set; } = null!;

    public string MenuImagesCollectionName { get; set; } = null!;
}