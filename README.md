# My LEGO Collection

This is a Blazor WebAssembly application that helps users manage and explore their LEGO collections. The app integrates with the Rebrickable API to search for LEGO sets and allows users to manage collections, add new sets, and store them locally using the browser's local storage.

## Features

- **Search for LEGO sets**: Find LEGO sets by searching with keywords.
- **Pagination**: View sets with pagination support.
- **Manage Collections**: Create, delete, and manage LEGO collections.
- **Add sets to collections**: Add sets from the search results to a collection.
- **Local Storage**: The app uses local storage to save and load collections.
- **Light Design**: The app has a straight-to-the-point and responsive design.

## Technologies Used

- **Blazor WebAssembly**: The front-end framework used for building the application.
- **Rebrickable API**: The external API for searching LEGO sets and retrieving set details.
- **C#**: The primary programming language for the backend logic.
- **Local Storage**: Used to store the user's LEGO collections in the browser.
- **CSS**: For styling the application.
- **xUnit**: For unit testing.
- **bUnit**: For testing Blazor components.

## Installation and Setup

### Prerequisites

- **.NET 6.0+**: The application is built using **.NET 6.0** (or a later version).
- **Rebrickable API key**: You will need a Rebrickable API key to interact with the LEGO sets API.

### Steps to Run the Application Locally

1. Clone the repository:

```bash
   git clone https://github.com/enzoscarpa-fnk/MyLegoCollection.git
   cd MyLegoCollection
```

2.	Open the project in your IDE (e.g., Visual Studio Code, JetBrains Rider).

3.	Create the appsettings.json file in /wwwroot and add your Rebrickable API key:

```json
  {
    "Rebrickable": {
      "ApiKey": "YOUR_API_KEY"
    }
  }
```

4.	Restore the dependencies:

```bash
     dotnet restore
```

5.	Build the project:

```bash
     dotnet build
```

6.	Run the application:

```bash
     dotnet run
```

7.	Open your browser and navigate to http://localhost:5000 to view the application.

## Key Notes

- **Search Functionality**: The app uses the Rebrickable API to search LEGO sets by keywords. The results are paginated to ensure smooth browsing.
- **Managing Collections**: Users can create collections and add sets to them. The collections are saved locally in the browser using local storage.
- **Testing**: The app includes unit tests for logic and component tests using bUnit.
