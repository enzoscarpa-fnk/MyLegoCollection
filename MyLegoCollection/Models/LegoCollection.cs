using System.Collections.Generic;

namespace MyLegoCollection.Models
{
    public class LegoCollection
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public List<LegoSet> Sets { get; set; } = new();
    }
}