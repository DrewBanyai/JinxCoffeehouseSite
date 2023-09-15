using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace REST_API.DTOs;

public class MenuItemDTO
{
    [BsonElement("Name")]
    public string Name {get; set;} = "MenuItem";

    public MenuItemType Type {get; set;} = MenuItemType.UNKNOWN;

    public string? Description {get; set;} = null;
}