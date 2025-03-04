using System.Collections.Generic;

namespace MyLegoCollection.Models
{
    public class LegoCollection
    {
        public string Name { get; set; }
        public List<LegoSet> Sets { get; set; } = new();
    }
}