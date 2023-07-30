namespace vertical_slice_example.Features.Books.Queries;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Publisher { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
}