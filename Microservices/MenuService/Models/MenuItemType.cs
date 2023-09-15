    using System.Text.Json.Serialization;
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MenuItemType {
        UNKNOWN,
        DRINK,
        FOOD,
        OTHER
    };