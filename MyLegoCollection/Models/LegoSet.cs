using System.Text.Json.Serialization;

namespace MyLegoCollection.Models
{
    public class LegoSet
    {
        [JsonPropertyName("set_num")]
        public string SetNum { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("num_parts")]
        public int NumParts { get; set; }

        [JsonPropertyName("set_img_url")]
        public string SetImgUrl { get; set; }
    }

    public class LegoSetResponse
    {
        [JsonPropertyName("results")]
        public List<LegoSet> Results { get; set; }
    }
}