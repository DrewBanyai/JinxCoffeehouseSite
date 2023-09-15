using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace REST_API.Models;

public class MenuItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set; }

    public DateTime CreatedAt {get; set;} = new DateTime(0);

    public DateTime LastEditedAt {get; set;} = new DateTime(0);

    [BsonElement("Name")]
    public string Name {get; set;} = "MenuItem";

    public MenuItemType Type {get; set;} = MenuItemType.UNKNOWN;

    public string? Description {get; set;} = null;
}