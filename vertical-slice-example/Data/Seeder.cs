using System.Text.Json;
using vertical_slice_example.Domain;

namespace vertical_slice_example.Data;

public static class Seeder
{
    public static async Task SeedAsync(DataContext dbContext)
    {
        var path = Directory.GetCurrentDirectory();

        if (!dbContext.Genres.Any())
        {
            var genresData = await File.ReadAllTextAsync(path + "/Data/SeedData/genresData.json");
            var genres = JsonSerializer.Deserialize<List<Genre>>(genresData, new JsonSerializerOptions(){PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            if (genres != null) dbContext.Genres.AddRange(genres);
        }
        
        if (!dbContext.Authors.Any())
        {
            var authorsData = await File.ReadAllTextAsync(path + "/Data/SeedData/authorsData.json");
            var authors = JsonSerializer.Deserialize<List<Author>>(authorsData, new JsonSerializerOptions(){PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            if (authors != null) dbContext.Authors.AddRange(authors);
        }
        
        if (!dbContext.Books.Any())
        {
            var booksData = await File.ReadAllTextAsync(path + "/Data/SeedData/booksData.json");
            var books = JsonSerializer.Deserialize<List<Book>>(booksData, new JsonSerializerOptions(){PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            if (books != null) dbContext.Books.AddRange(books);
        }
        
        await dbContext.SaveChangesAsync();
    }
}