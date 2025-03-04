using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyLegoCollection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register the Blazored LocalStorage service for storing data in the browser
builder.Services.AddBlazoredLocalStorage();

// Register an HTTP client with the base address of the application
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register the RebrickableService to interact with the Rebrickable API
builder.Services.AddScoped<RebrickableService>();

// Load the application settings from the appsettings.json file
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Retrieve the Rebrickable API key from the configuration
var apiKey = config["Rebrickable:ApiKey"];

// Register the RebrickableService with the retrieved API key
builder.Services.AddScoped(sp => new RebrickableService(
    sp.GetRequiredService<HttpClient>(), apiKey
));

// Register the CollectionService to manage LEGO collections
builder.Services.AddScoped<CollectionService>();

await builder.Build().RunAsync();