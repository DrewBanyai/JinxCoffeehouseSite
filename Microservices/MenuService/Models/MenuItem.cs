using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace REST_API.Models;

public class MenuItem
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MenuItemType {
        UNKNOWN,
        DRINK,
        FOOD,
        OTHER
    };

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set; }

    [BsonElement("Name")]
    public string Name {get; set;} = "MenuItem";

    public MenuItemType Type {get; set;} = MenuItemType.UNKNOWN;

    public string? Description {get; set;} = null;
}