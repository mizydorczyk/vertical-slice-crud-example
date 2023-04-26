namespace vertical_slice_example.Domain;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Publisher { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }

    public Genre Genre { get; set; }
    public int GenreId { get; set; }

    public Author Author { get; set; }
    public int AuthorId { get; set; }
}