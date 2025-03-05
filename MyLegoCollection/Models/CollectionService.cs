using Blazored.LocalStorage;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLegoCollection.Models
{
    public class CollectionService
    {
        private readonly ILocalStorageService _localStorage;
        public List<LegoCollection> Collections { get; private set; } = new();

        // Constructor injecting the local storage service
        public CollectionService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        // Loads collections from local storage
        public async Task LoadCollectionsAsync()
        {
            var json = await _localStorage.GetItemAsStringAsync("collections");
        
            // If data exists, deserialize it into a list of collections
            if (!string.IsNullOrEmpty(json))
            {
                Collections = JsonSerializer.Deserialize<List<LegoCollection>>(json) ?? new List<LegoCollection>();
            }
        }

        // Saves the current collections list to local storage
        public async Task SaveCollectionsAsync()
        {
            var json = JsonSerializer.Serialize(Collections);
            await _localStorage.SetItemAsStringAsync("collections", json);
        }

        // Returns the current list of collections
        public List<LegoCollection> GetCollections()
        {
            return Collections;
        }

        // Adds a new collection to the list and saves it to local storage
        public async Task AddCollection(LegoCollection newCollection)
        {
            Collections.Add(newCollection);
            await SaveCollectionsAsync();
        }
        
        // Adds a new collection to the list and saves it to local storage
        public async Task DeleteCollection(LegoCollection collectionToDelete)
        {
            Collections.Remove(collectionToDelete);
            await SaveCollectionsAsync();
        }
    }
}