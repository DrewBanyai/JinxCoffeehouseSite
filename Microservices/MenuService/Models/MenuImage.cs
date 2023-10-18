using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MenuService.Models;

public class MenuImage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set; } = null;

    public DateTime CreatedAt {get; set;} = new DateTime(0);

    public DateTime LastEditedAt {get; set;} = new DateTime(0);

    [BsonElement("Name")]
    public string Name {get; set;} = "NAME";

    public string ImageBase64 {get; set;} = "";
}