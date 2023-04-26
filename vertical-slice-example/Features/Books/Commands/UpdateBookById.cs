using MediatR;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Exceptions;

namespace vertical_slice_example.Features.Books.Commands;

public class UpdateBookByIdCommand : IRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Publisher { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
}

public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand>
{
    private readonly DataContext _dbContext;

    public UpdateBookByIdCommandHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == request.Id);
        if (book == null) throw new NotFoundException("Book not found");

        book.Title = request.Title;
        book.Publisher = request.Publisher;
        book.Description = request.Description;
        book.ReleaseDate = request.ReleaseDate;
        book.AuthorId = request.AuthorId;
        book.GenreId = request.GenreId;

        _dbContext.Update(book);
        await _dbContext.SaveChangesAsync();
    }
}