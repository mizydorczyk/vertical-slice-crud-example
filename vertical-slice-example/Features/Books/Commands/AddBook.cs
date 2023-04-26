using MediatR;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Domain;
using vertical_slice_example.Exceptions;

namespace vertical_slice_example.Features.Books.Commands;

public class AddBookCommand : IRequest<Book>
{
    public string Title { get; set; }
    public string Publisher { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
}

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
{
    private readonly DataContext _dbContext;

    public AddBookCommandHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        if (await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == request.AuthorId) == null) throw new NotFoundException("AuthorId is incorrect");

        if (await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == request.AuthorId) == null) throw new NotFoundException("GenreId is incorrect");

        var book = new Book
        {
            Title = request.Title,
            Publisher = request.Publisher,
            Description = request.Description,
            ReleaseDate = request.ReleaseDate,
            AuthorId = request.AuthorId,
            GenreId = request.GenreId
        };

        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();

        return book;
    }
}