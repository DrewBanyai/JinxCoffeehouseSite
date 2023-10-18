using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MenuService.DTOs;

public class MenuItemDTO
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set; } = null;
    
    [BsonElement("Name")]
    public string Name {get; set;} = "NAME";

    public MenuItemType Type {get; set;} = MenuItemType.UNKNOWN;

    public string Description {get; set;} = "";

    public List<string> Tags {get; set;} = new List<string>();

    public List<string> PossibleAlterations {get; set;} = new List<string>();

    //  Note: Price unit is in pennies, so a value of 250 would mean the price is $2.50
    public int Price {get; set;} = 0;
}