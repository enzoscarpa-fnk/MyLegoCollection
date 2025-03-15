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

        // Loads collections when initialized
        public async Task InitializeAsync()
        {
            await LoadCollectionsAsync();
        }
        
        // Loads collections from local storage
        public async Task LoadCollectionsAsync()
        {
            // Uses GetItemAsync method to load collections straight from LocalStorage
            Collections = await _localStorage.GetItemAsync<List<LegoCollection>>("collections") ?? new List<LegoCollection>();
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
        
        // Deletes a collection in the list and removes it from local storage
        public async Task DeleteCollection(string collectionId)
        {
            var collectionToRemove = Collections.FirstOrDefault(c => c.Id == collectionId);
            if (collectionToRemove != null)
            {
                Collections.Remove(collectionToRemove);
                await SaveCollectionsAsync();
            }
        }
        
        // Adds a set to a selected collection and saves it to local storage
        public async Task AddSetToCollection(string collectionId, LegoSet set)
        {
            var collection = Collections.FirstOrDefault(c => c.Id == collectionId);
            if (collection != null)
            {
                if (!collection.Sets.Any(s => s.SetNum == set.SetNum)) // Verification to avoid duplicate sets
                {
                    collection.Sets.Add(set);
                    await SaveCollectionsAsync();
                }
            }
        }
        
        // Removes a set from a selected collection in local storage
        public async Task RemoveSetFromCollection(string collectionId, string setNum)
        {
            var collection = Collections.FirstOrDefault(c => c.Id == collectionId);
    
            if (collection != null)
            {
                collection.Sets.RemoveAll(s => s.SetNum == setNum);
        
                await SaveCollectionsAsync();
            }
        }
    }
}