using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace REST_API.DTOs;

public class MenuImageDTO
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set; } = null;
    
    [BsonElement("Name")]
    public string Name {get; set;} = "NAME";

    public string ImageBase64 {get; set;} = "";
}